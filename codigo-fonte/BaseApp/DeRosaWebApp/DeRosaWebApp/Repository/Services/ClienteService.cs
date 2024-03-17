using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class ClienteService: IClienteService
    {
        #region Propriedades, Construtor e injeção de dependencia
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Get Cliente pelo ID

        public async Task<Cliente> GetClienteByIdAsync(string id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id_User == id);
        }
        #endregion
        #region Atualizar cliente

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        #endregion
    }
}
