using DeRosaWebApp.Repository.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DeRosaWebApp.Repository.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp-mail.outlook.com";
        private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "derosateste@hotmail.com";
        private readonly string _senderPassword = "adminderosa1234";
        private readonly string _nome = "DeRosa";

        public async Task SendEmailAsync(string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml)
        {
            try
            {
                if (!IsValidEmail(emailDestinatario))
                {
                    throw new ArgumentException("O e-mail destinatário é inválido.");
                }

                MailMessage mail = new MailMessage(){
                    From = new MailAddress(_senderEmail, _nome)
                };

                mail.To.Add(emailDestinatario);
                mail.Subject = assunto;
                mail.Body = mensagemHtml;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                
                var smtp = new SmtpClient(_smtpServer, _smtpPort);
                smtp.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(mail);
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
