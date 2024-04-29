using DeRosaWebApp.Repository.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DeRosaWebApp.Repository.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "teste@gmail.com";
        private readonly string _senderPassword = Environment.GetEnvironmentVariable("GmailPassword");

        public async Task SendEmailAsync(string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml)
        {
            try
            {
                if (!IsValidEmail(emailDestinatario))
                {
                    throw new ArgumentException("O e-mail destinatário é inválido.");
                }

                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                    EnableSsl = true,
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_senderEmail),
                    Subject = assunto,
                    Body = mensagemHtml,
                    IsBodyHtml = true,
                };

                message.To.Add(emailDestinatario);

                await smtpClient.SendMailAsync(message);
                
                Console.WriteLine($"E-mail enviado para: {emailDestinatario}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                throw;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
