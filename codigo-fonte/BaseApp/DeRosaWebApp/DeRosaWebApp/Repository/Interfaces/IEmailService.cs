using System.Threading.Tasks;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml);
    }
}