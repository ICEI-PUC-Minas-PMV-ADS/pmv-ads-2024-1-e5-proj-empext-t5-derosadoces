using DeRosaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetAllCategorias();
        Task<ActionResult<IEnumerable<Produto>>> GetByCategoria(int id);
        IQueryable<Categoria> PaginationCategoria();
        Task<Categoria> GetById(int id);
        Task Update(int id, Categoria categoria);
        Task Delete(int id);
        Task<bool> Any(int id);
        Task Add(Categoria categoria);
        string GetNameById(int idCategoria);
    }
}
