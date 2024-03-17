using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class CategoriaService : ICategoriaService
    {
        #region Construtor, injeção de dependência e propriedades
        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        #endregion
        #region Get todas as categorias
        public IEnumerable<Categoria> GetAllCategorias()
        {
            return _context.Categorias.ToList();
        }
        #endregion
        #region Get Categoria pelo id

        public async Task<ActionResult<IEnumerable<Produto>>> GetByCategoria(int idCategoria)
        {
            var list = await _context.Produtos.Where(p => p.IdCategoria == idCategoria).ToListAsync();
            if (list is not null)
            {
                return list;
            }
            return new NotFoundObjectResult("Sem categorias nesse id");
        }
        #endregion
        #region Query de categorias para paginação
        public IQueryable<Categoria> PaginationCategoria()
        {
            var ctg = _context.Categorias.AsNoTracking().AsQueryable();
            return ctg;
        }
        #endregion
    }
}
