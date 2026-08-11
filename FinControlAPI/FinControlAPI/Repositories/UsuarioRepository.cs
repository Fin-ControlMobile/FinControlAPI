using FinControlAPI.Contexts;
using FinControlAPI.Domains;
using FinControlAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinControlAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FinControlDbContext _context;

        public UsuarioRepository(FinControlDbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.AsNoTracking().ToList();
        }

        public Usuario ObterPorId(int id)
        {
            return _context.Usuario.Find(id);
        }

        public bool emailExistente(string email)
        {
            return _context.Usuario.Any(u => u.email == email);
        }

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

    }
}
