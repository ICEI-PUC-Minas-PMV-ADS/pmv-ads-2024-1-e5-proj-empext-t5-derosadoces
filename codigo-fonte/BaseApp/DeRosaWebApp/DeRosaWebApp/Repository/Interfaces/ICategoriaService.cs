using DeRosaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetAllCategorias();
        Task<ActionResult<IEnumerable<Produto>>> GetByCategoria(int id);
        IQueryable<Categoria> PaginationCategoria();
    }
}
