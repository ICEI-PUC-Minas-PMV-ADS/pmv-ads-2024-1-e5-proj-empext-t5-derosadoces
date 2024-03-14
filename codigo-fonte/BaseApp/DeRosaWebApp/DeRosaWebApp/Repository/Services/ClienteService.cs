using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class ClienteService: IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> GetClienteByIdAsync(string id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id_User == id);
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
