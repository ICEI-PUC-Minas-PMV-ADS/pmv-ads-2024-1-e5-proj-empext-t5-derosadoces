using DeRosaWebApp.Models.Management;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IManageSite
    {
        Task AlterarTituloDaSemana(string titulo);
        Task AlterarTextoSobre(string texto);
        Task AlterarTituloSobre(string texto);
        Task VerifyIsNull();
        Task UpdateDefault();
        Task<ManagementSobre> GetManagementSobre();
        Task<ManagementHome> GetManagementsHome();

    }
}
