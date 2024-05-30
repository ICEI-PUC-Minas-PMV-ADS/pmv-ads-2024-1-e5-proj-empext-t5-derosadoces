using DeRosaWebApp.Models.Management;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IManageSite
    {
        Task AlterarTituloDaSemana(ManagementHome titulo);
        Task AlterarTextoSobre(ManagementSobre texto);
        Task AlterarTituloSobre(ManagementSobre texto);
        Task VerifyIsNull();
        Task UpdateDefault();
        Task<ManagementSobre> GetManagementSobre();
        Task<ManagementHome> GetManagementsHome();

    }
}
