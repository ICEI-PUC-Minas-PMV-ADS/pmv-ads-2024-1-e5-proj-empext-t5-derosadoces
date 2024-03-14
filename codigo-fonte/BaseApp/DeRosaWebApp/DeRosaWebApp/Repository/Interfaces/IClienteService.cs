using DeRosaWebApp.Models;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente> GetClienteByIdAsync(string id);
        Task<Cliente> UpdateClienteAsync(Cliente cliente);
    }
}
