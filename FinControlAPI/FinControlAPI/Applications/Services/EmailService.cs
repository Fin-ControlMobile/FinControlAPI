using FinControlAPI.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FinControlAPI.Applications.Services
{
    public class EmailService : IEmailService
    {
        public async Task EnviarEmailRedefinicaoSenhaAsync(
            string email,
            string token)
        {
            var host = Environment.GetEnvironmentVariable("EMAIL_HOST");
            var portString = Environment.GetEnvironmentVariable("EMAIL_PORT");
            var usuario = Environment.GetEnvironmentVariable("EMAIL_USER");
            var senha = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
            var remetente = Environment.GetEnvironmentVariable("EMAIL_FROM");

            if (string.IsNullOrWhiteSpace(host) ||
                string.IsNullOrWhiteSpace(portString) ||
                string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(remetente))
            {
                throw new InvalidOperationException(
                    "As configurações de e-mail não foram encontradas."
                );
            }

            if (!int.TryParse(portString, out int porta))
            {
                throw new InvalidOperationException(
                    "EMAIL_PORT inválido."
                );
            }

            var mensagem = new MimeMessage();

            // Remetente
            mensagem.From.Add(
                new MailboxAddress(
                    "FinControl",
                    remetente
                )
            );

            // Destinatário
            mensagem.To.Add(
                MailboxAddress.Parse(email)
            );

            // Assunto
            mensagem.Subject = "Redefinição de senha - FinControl";

            // Corpo do e-mail
            mensagem.Body = new TextPart("plain")
            {
                Text =
                    "Olá!\n\n" +
                    "Recebemos uma solicitação para redefinir sua senha.\n\n" +
                    "Seu código de redefinição é:\n\n" +
                    $"{token}\n\n" +
                    "Esse código é válido por 60 minutos.\n\n" +
                    "Caso você não tenha solicitado a redefinição, ignore este e-mail.\n\n" +
                    "Equipe FinControl"
            };

            using var smtp = new SmtpClient();

            // Conecta ao servidor SMTP
            await smtp.ConnectAsync(
                host,
                porta,
                SecureSocketOptions.SslOnConnect
            );

            // Autentica
            await smtp.AuthenticateAsync(
                usuario,
                senha
            );

            // Envia
            await smtp.SendAsync(mensagem);

            // Desconecta
            await smtp.DisconnectAsync(true);
        }
    }
}