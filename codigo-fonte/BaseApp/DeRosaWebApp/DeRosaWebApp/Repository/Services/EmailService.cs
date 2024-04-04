using DeRosaWebApp.Repository.Interfaces;

namespace DeRosaWebApp.Repository.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml)
        {
            throw new NotImplementedException();
        }
    }
}
