using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly AppDbContext _context;
        public EnderecoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Endereco> Create(Endereco endereco)
        {
            if(endereco is not null)
            {
                _context.Enderecos.Add(endereco);
               await  _context.SaveChangesAsync();
            }
            return endereco;
        }
        public async Task Update(Endereco e)
        {
            if(e is not null)
            {
                _context.Enderecos.Update(e);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            return await _context.Enderecos.FirstAsync(x => x.Id == id);      
        }

        public async Task<Endereco> GetEnderecoByUser(string user)
        {
            return await _context.Enderecos.FirstAsync(p => p.Id_User == user);
        }

        public async Task<List<Endereco>> GetListaEnderecoUsuario(string id_User)
        {
            var enderecos = await _context.Enderecos.Where(p => p.Id_User == id_User).ToListAsync();
            if(enderecos is null)
            {
                return (List<Endereco>)Enumerable.Empty<List<Endereco>>();
            }
            return enderecos;
         
        }
    }
}
