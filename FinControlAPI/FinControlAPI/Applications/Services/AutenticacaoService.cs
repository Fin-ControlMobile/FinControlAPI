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

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt tokenjWT)
        {
            _repository = repository;
            _tokenJwt = tokenjWT;
        }

        // Compara a SHA256
        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Usuario usuario = _repository.ObterPorEmail(loginDto.Email);

            if (usuario == null)
            {
                throw new DomainException("E-mail ou senha inválidos.");
            }

            // Compara a senha digitada com a senha armazenada
            if (!VerificarSenha(loginDto.Senha, usuario.senha))
            {
                throw new DomainException("E-mail ou senha inválidos.");
            }

            // Geração do token
            var token = _tokenJwt.GerarToken(usuario);

            TokenDto novoToken = new TokenDto { token = token };

            return novoToken;
        }
    }
}
