using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinControlAPI.Domains;
using FinControlAPI.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace VH_Burguer.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            // KEY -> Chave secreta de assinatura do token que garante que ele NÃO foi alterado depois de ser gerado
            var chave = Environment.GetEnvironmentVariable("JWT_KEY")!;

            // ISSUER -> Emissor do token (nome da API / sistema gerador)
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;

            // AUDIENCE -> Para quem o token foi criado. Define qual sistema pode usar o token
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;

            // Tempo de EXPIRAÇÃO em MINUTOS
            var expiraEmMinutos = int.Parse(
    Environment.GetEnvironmentVariable("JWT_EXPIRA_EM_MINUTOS")!);

            // CONVERTE a chave para BYTES
            var keyBytes = Encoding.UTF8.GetBytes(chave);

            // SEGURANÇA: mínimo de 32 bytes para a chave
            if (keyBytes.Length < 32)
            {
                throw new DomainException("A chave deve conter no mínimo 32 caracteres (bytes)");
            }

            // Cria a chave de segurança usada para ASSINAR o TOKEN
            var securityKey = new SymmetricSecurityKey(keyBytes);

            // Define o algoritmo de assinatura do token
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims -> Informações do usuário que estarão no token
            // Podem ser recuperadas na API para identificar o usuário
            var claims = new List<Claim>
            {
                // ID do usuário
                new Claim(ClaimTypes.NameIdentifier, usuario.usuarioId.ToString()),

                // Nome do usuário
                new Claim(ClaimTypes.Name, usuario.nome),

                // Email do usuário
                new Claim(ClaimTypes.Email, usuario.email)
            };

            // Cria o token JWT com as informações definidas
            var token = new JwtSecurityToken(
                issuer: issuer, // Quem gerou
                audience: audience, // Quem pode usar
                claims: claims, // Dados do usuário
                expires: DateTime.Now.AddMinutes(expiraEmMinutos), // Validade do token
                signingCredentials: credentials // Assinatura do token
            );

            // Converte o token para string e retorna ao cliente
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}