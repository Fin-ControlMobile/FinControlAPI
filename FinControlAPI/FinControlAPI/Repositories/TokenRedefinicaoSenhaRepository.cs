using FinControlAPI.Contexts;
using FinControlAPI.Domains;
using FinControlAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinControlAPI.Repositories
{
    public class TokenRedefinicaoSenhaRepository : ITokenRedefinicaoSenhaRepository
    {
        private readonly FinControlDbContext _context;

        public TokenRedefinicaoSenhaRepository(FinControlDbContext context)
        {
            _context = context;
        }

        public async Task CadastrarAsync(TokenRedefinicaoSenha token)
        {
            _context.TokenRedefinicaoSenhas.Add(token);

            await _context.SaveChangesAsync();
        }

        public async Task<TokenRedefinicaoSenha?> BuscarPorHashAsync(string tokenHash)
        {
            return await _context.TokenRedefinicaoSenhas.FirstOrDefaultAsync(t => t.TokenHash == tokenHash && !t.Utilizado);
        }

        public async Task AtualizarAsync(TokenRedefinicaoSenha token)
        {
            _context.TokenRedefinicaoSenhas.Update(token);

            await _context.SaveChangesAsync();
        }
    }
}
