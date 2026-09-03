namespace FinControlAPI.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailRedefinicaoSenhaAsync(
            string email,
            string token
        );
    }
}