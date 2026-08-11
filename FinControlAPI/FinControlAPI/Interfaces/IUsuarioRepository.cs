using FinControlAPI.Domains;

namespace FinControlAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario ObterPorId(int id);

        bool emailExistente(string email);

        void Cadastrar(Usuario usuario);
    }
}
