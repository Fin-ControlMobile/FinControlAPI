using FinControlAPI.Domains;
using FinControlAPI.DTOs.UsuarioDto;
using FinControlAPI.Exceptions;
using FinControlAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FinControlAPI.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public LerUsuarioDto ConverterParaDto(Usuario usuario)
        {
            return new LerUsuarioDto
            {
                usuarioId = usuario.usuarioId,
                nome = usuario.nome,
                email = usuario.email,
                saldo = usuario.saldo,
                primeiroAcesso = usuario.primeiroAcesso
            };
        }

        private static byte[] CriptografarSenha(string senha)
        {
            using var sha256 = SHA256.Create(); // Gera hash e devolve em byte

            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<LerUsuarioDto> usuariosDto = new List<LerUsuarioDto>();

            foreach (var usuario in usuarios)
            {
                usuariosDto.Add(ConverterParaDto(usuario));
            }

            List<LerUsuarioDto> listaUsuario = _repository.Listar().Select(u => ConverterParaDto(u)).ToList();

            return usuariosDto;
        }

        private static bool NomePossuiNumeros(string nome)
        {
            foreach (char caractere in nome)
            {
                if (char.IsDigit(caractere))
                {
                    return true;
                }
            }

            return false;
        }

        public LerUsuarioDto ObterPorId(Guid id)
        {
            Usuario usuario = _repository.ObterPorId(id);
            if (usuario == null)
            {
                return null;
            }
            return ConverterParaDto(usuario);
        }

        public bool EmailExistente(string email)
        {
            return _repository.emailExistente(email);
        }

        public void Adicionar(CriarUsuarioDto usuarioDto)
        {
            if (string.IsNullOrWhiteSpace(usuarioDto.email))
            {
                throw new DomainException("O email é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(usuarioDto.senha))
            {
                throw new DomainException("A senha é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(usuarioDto.nome))
            {
                throw new DomainException("O nome é obrigatório.");
            }

            if (usuarioDto.senha.Length < 8)
            {
                throw new DomainException("A senha deve ter pelo menos 8 caracteres.");
            }

            if (!usuarioDto.email.Contains("@"))
            {
                throw new DomainException("Insira um email válido.");
            }

            if (usuarioDto.email.Length < 11)
            {
                throw new DomainException("Insira um email válido.");
            }

            if (NomePossuiNumeros(usuarioDto.nome))
            {
                throw new DomainException("O nome não pode conter números.");
            }

            if (EmailExistente(usuarioDto.email))
            {
                throw new DomainException("O email já está em uso.");
            }

            Usuario usuario = new Usuario
            {
                nome = usuarioDto.nome,
                email = usuarioDto.email,
                senha = CriptografarSenha(usuarioDto.senha),
            };

            _repository.Cadastrar(usuario);
        }
    }
}