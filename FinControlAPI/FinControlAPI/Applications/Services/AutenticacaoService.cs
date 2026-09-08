using FinControlAPI.Applications.Autenticacao;
using FinControlAPI.Domains;
using FinControlAPI.DTOs.AutenticacaoDto;
using FinControlAPI.Exceptions;
using FinControlAPI.Interfaces;
using VH_Burguer.Applications.Autenticacao;

namespace FinControlAPI.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;
        private readonly GeradorTokenRecuperacaoSenha _tokenSenha;
        private readonly ITokenRedefinicaoSenhaRepository _tokenRepository;
        private readonly IEmailService _emailService;

        public AutenticacaoService(
            IUsuarioRepository repository,
            GeradorTokenJwt tokenJwt,
            ITokenRedefinicaoSenhaRepository tokenRepository,
            GeradorTokenRecuperacaoSenha tokenSenha,
            IEmailService emailService)
        {
            _repository = repository;
            _tokenJwt = tokenJwt;
            _tokenRepository = tokenRepository;
            _tokenSenha = tokenSenha;
            _emailService = emailService;
        }

        // Compara a SHA256
        private static bool VerificarSenha(
            string senhaDigitada,
            byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();

            var hashDigitado = sha.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(senhaDigitada)
            );

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            Usuario usuario = await _repository.ObterPorEmail(loginDto.Email);

            if (usuario == null)
            {
                throw new DomainException(
                    "E-mail ou senha inválidos."
                );
            }

            if (!VerificarSenha(loginDto.Senha, usuario.senha))
            {
                throw new DomainException(
                    "E-mail ou senha inválidos."
                );
            }

            var token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto
            {
                token = token
            };

            if(_repository.VeriificarPrimeiroAcesso(usuario.usuarioId))
            {
                _repository.AtualizarPrimeiroAcesso(usuario.usuarioId);
            }

            return novoToken;
        }

        public async Task<bool> SolicitarRedefinicaoSenhaAsync(
            SolicitarRedefinicaoSenhaDto dto)
        {
            Usuario usuario = await _repository.ObterPorEmail(dto.Email);

            if (usuario == null)
            {
                return false;
            }

            // Gera o token original
            var token = _tokenSenha.GerarToken();

            // Gera o hash que será salvo no banco
            var tokenHash = _tokenSenha.GerarHashToken(token);


            // Cria o registro do token
            TokenRedefinicaoSenha tokenRedefinicao =
                new TokenRedefinicaoSenha
                {
                    UsuarioId = usuario.usuarioId,
                    TokenHash = tokenHash,
                    ExpiraEm = DateTime.UtcNow.AddMinutes(60),
                    Utilizado = false
                };

            // Salva o hash no banco
            await _tokenRepository.CadastrarAsync(tokenRedefinicao);


            // Envia o token original para o e-mail do usuário
            await _emailService.EnviarEmailRedefinicaoSenhaAsync(
                    usuario.email,
                    token
            );
     

            return true;
        }

        public async Task<bool> RedefinirSenhaAsync(
            RedefinirSenhaDto dto)
        {
            var usuario = await _repository.ObterPorEmail(dto.Email);

            if (usuario == null)
                return false;

            // Gera o hash do token recebido pelo usuário
            var tokenHash = _tokenSenha.GerarHashToken(dto.Token);

            // Procura o hash no banco
            var token = await _tokenRepository.BuscarPorHashAsync(
                tokenHash
            );

            if (token == null)
                return false;

            if (token.UsuarioId != usuario.usuarioId)
                return false;

            if (token.Utilizado)
                return false;

            if (token.ExpiraEm < DateTime.UtcNow)
                return false;

            // Criptografa a nova senha
            usuario.senha = CriptografiaUsuario.CriptografarSenha(
                dto.NovaSenha
            );

            // Atualiza a senha
            await _repository.AtualizarAsync(usuario);

            // Marca o token como utilizado
            token.Utilizado = true;

            await _tokenRepository.AtualizarAsync(token);

            return true;
        }
    }
}