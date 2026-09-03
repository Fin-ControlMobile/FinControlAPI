using FinControlAPI.Domains;

namespace FinControlAPI.Interfaces
{
    public interface ITokenRedefinicaoSenhaRepository
    {
        Task CadastrarAsync(TokenRedefinicaoSenha token);

        Task<TokenRedefinicaoSenha?> BuscarPorHashAsync(
            string tokenHash
        );

        Task AtualizarAsync(TokenRedefinicaoSenha token);
    }
}
