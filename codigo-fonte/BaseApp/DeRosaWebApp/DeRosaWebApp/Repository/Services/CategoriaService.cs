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
        #region Get List Categoria pelo id

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
        #region Get By Id
        public async Task<Categoria> GetById(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.IdCategoria == id);
            return categoria;
        }
        #endregion
        #region Atualizar categoria
        public async Task Update(int id , Categoria categoria)
        {
            
            var categoriaDb = await _context.Categorias.FirstOrDefaultAsync(p => p.IdCategoria == id);
            if(categoriaDb is not null)
            {
                categoriaDb.CategoriaNome = categoria.CategoriaNome;
                _context.Categorias.Update(categoriaDb);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
        #region Deletar
        public async Task Delete(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.IdCategoria == id);
            if(categoria is not null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
        #region Any
        public async Task<bool> Any(int id)
        {
            return await _context.Categorias.AnyAsync(p => p.IdCategoria == id);
        }
        #endregion
        #region Add
        public async Task Add(Categoria categoria)
        {
            if(categoria is not null)
            {
                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
