using DeRosaWebApp.Models;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> Create(Endereco endereco);
        Task<Endereco> GetEnderecoById(int id);
        Task<Endereco> GetEnderecoByUser(string user);
        Task<List<Endereco>> GetListaEnderecoUsuario(string id_User);
        Task Update(Endereco e);
    }
}
