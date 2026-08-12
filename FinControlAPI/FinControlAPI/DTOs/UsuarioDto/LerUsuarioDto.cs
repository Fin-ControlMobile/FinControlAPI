namespace FinControlAPI.DTOs.UsuarioDto
{
    public class LerUsuarioDto
    {
        public string nome { get; set; }

        public string email { get; set; }

        public decimal saldo { get; set; }

        public bool primeiroAcesso { get; set; }
    }
}
