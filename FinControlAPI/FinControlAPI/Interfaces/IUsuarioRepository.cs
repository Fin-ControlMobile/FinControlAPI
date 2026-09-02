using FinControlAPI.Domains;

namespace FinControlAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario ObterPorId(Guid id);

        Usuario ObterPorEmail(string email);

        bool emailExistente(string email);

        void Cadastrar(Usuario usuario);
    }
}
