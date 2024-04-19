using DeRosaWebApp.Models;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente> GetClienteByIdAsync(string id);
        Task<Cliente> UpdateClienteAsync(Cliente cliente);
        Task<Cliente> GetClienteByUserId(string userId);
        Task<Cliente> UpdateOnlyEnderecoId(int enderecoId, int clienteId);
    }
}
