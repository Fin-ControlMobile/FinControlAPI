using FinControlAPI.Domains;
using FinControlAPI.DTOs.AutenticacaoDto;

namespace FinControlAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario ObterPorId(Guid id);
        Task<Usuario> ObterPorEmail(string email);
        bool emailExistente(string email);
        void Cadastrar(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        bool VeriificarPrimeiroAcesso(Guid usuarioId);
        void AtualizarPrimeiroAcesso(Guid usuarioId);
    }
}
