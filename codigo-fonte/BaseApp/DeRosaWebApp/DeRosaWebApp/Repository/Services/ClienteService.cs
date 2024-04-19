using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        #region Get Cliente pelo userId
        public async Task<Cliente> GetClienteByUserId(string user_id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(p => p.Id_User == user_id);
        }
        #endregion
        #region Update somente enderecoId
        public async Task<Cliente> UpdateOnlyEnderecoId(int enderecoId, int clienteId)
        {
            var cliente = await _context.Clientes.FindAsync(clienteId);

            cliente.IdEndereco = enderecoId;
            _context.Entry(cliente).Property(x => x.IdEndereco).IsModified = true;

            await _context.SaveChangesAsync();
            return cliente;  
        }
        #endregion
    }
}
