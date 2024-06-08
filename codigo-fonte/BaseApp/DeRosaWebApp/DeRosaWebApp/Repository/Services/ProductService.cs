using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class ProductService : IProductService
    {
        #region Propriedades, Construtor e injeção de dependência
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Any Produto existente (bool)
        public bool Any(int id)
        {
            var produto = _context.Produtos.Any(p => p.Cod_Produto == id);
            return produto;
        }
        #endregion
        #region Verificar a quantidade em estoque
        public async Task<int> QuantidadeEmEstoque(int cod_produto)
        {
            var quantidade = await _context.Produtos.Where(p => p.Cod_Produto == cod_produto).Select(p => p.EmEstoque).FirstOrDefaultAsync();
            return quantidade;    
        }

        #endregion
        #region Verificar a quantidade em estoque agendamento
        public async Task<int> QuantidadeEmEstoqueAgendamento(int cod_produto)
        {
            var quantidade = await _context.Produtos.Where(p => p.Cod_Produto == cod_produto).Select(p=> p.EstoqueAgendamento).FirstOrDefaultAsync();
            return quantidade;
        }

        #endregion
        #region Verifica quantidade total de produtos
        public int VerifyQnt()
        {
            var produtosQnt = _context.Produtos.Count();
            return produtosQnt;
        }
        #endregion
        #region Adicionar um produto teste
        public async Task<ActionResult<Produto>> AddTestProduct(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return new OkObjectResult(produto);
        }
        #endregion
        #region Criar um produto
        public async Task<IActionResult> Create(Produto produto)
        {
            if (produto is not null)
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return new OkObjectResult(produto);
            }
            else
            {
                return new BadRequestObjectResult(produto);
            }
        }
        #endregion
        #region Deletar produto pelo ID

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Cod_Produto == id);
            if (produto is not null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return new OkObjectResult(produto);
            }
            else
            {
                return new BadRequestObjectResult(produto);
            }
        }
        #endregion
        #region Produtos em Query para paginação
        public IQueryable<Produto> PaginationProduct()
        {
            var result = _context.Produtos.AsNoTracking().AsQueryable();
            return result;
        }
        #endregion
        #region Produtos em Query para paginação Home
        public IQueryable<Produto> PaginationProductHome()
        {
            var result = _context.Produtos.AsNoTracking().AsQueryable().Take(4);
            return result;
        }
        #endregion
        #region Get todos os produtos
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return produtos;

        }
        #endregion
        #region Get produto pelo ID
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Cod_Produto == id);
            if (produto is not null)
            {
                return produto;
            }

            return new NotFoundObjectResult("Produto não encontrado!");
        }
        #endregion
        #region Atualizar produto
        public async Task<IActionResult> Update(Produto produto, int id)
        {
            var productExist = await _context.Produtos.FirstOrDefaultAsync(p => p.Cod_Produto == id);
            if (productExist is not null)
            {
                productExist.Cod_Produto = id;
                productExist.EmEstoque = produto.EmEstoque;
                productExist.DescricaoCurta = produto.DescricaoCurta;
                productExist.IdCategoria = produto.IdCategoria;
                productExist.ImagemUrl = produto.ImagemUrl;
                productExist.Nome = produto.Nome;
                productExist.Preco = produto.Preco;
                productExist.PrecoSecundario = produto.PrecoSecundario;
                productExist.ProdutoDaSemana = produto.ProdutoDaSemana;
                productExist.Indisponivel = produto.Indisponivel;
                productExist.EstoqueAgendamento = produto.EstoqueAgendamento;
                productExist.Promocional = produto.Promocional;

                _context.Produtos.Update(productExist);
                await _context.SaveChangesAsync();
                return new OkObjectResult(productExist);
            }

            else
            {
                return new BadRequestObjectResult(produto);
            }
        }

        #endregion
        #region Get produto pelo Nome
        public async Task<IEnumerable<Produto>> GetByName(string searchString)
        {
            var find = await GetAll();
            ActionResult<IEnumerable<Produto>> listNameLike = find;
            IEnumerable<Produto> listNameEmpty = Enumerable.Empty<Produto>();
            List<Produto> resultProd = new List<Produto>();
            foreach (Produto prod in listNameLike.Value)
            {
                if (Compare(searchString, prod.Nome))
                {
                    resultProd.Add(prod);
                }
            }
            if (resultProd is not null)
            {
                return resultProd;
            }
            return listNameEmpty;
        }
        #endregion
        #region Metodo de pesquisa, comparar uma string com outra
        public bool Compare(string search, string nameProd)
        {
            string[] wordsProdName = nameProd.Split(" ");  // todas as palavras do nome do produto em um vetor
            string[] compareStrings = search.Split(" "); // criação de um vetor para armazenar as palavras de pesquisa
            if (compareStrings.Length > 1) // se o vetor de pesquisa for maior que 1, ou seja, uma frase, então faremos a comparação:
            {
                for (int i = 1; i < wordsProdName.Length; i++)
                {
                    string wordConc = wordsProdName[i - 1].ToLower() + " " + wordsProdName[i].ToLower();
                    string compareConc = compareStrings[0].ToLower() + " " + compareStrings[1].ToLower();
                    if (wordConc == compareConc) // se as palavras formarem uma frase com pelo menos duas palavras, então, ele retorna esse produto
                    {
                        return true;
                    }
                }
            }
            foreach (string word in wordsProdName) // se for somente uma palavra, então ele faz a comparação:
            {
                foreach (string compareStrSearch in compareStrings)
                {
                    string wordProdLower = word.ToLower();
                    string wordSearchLower = compareStrSearch.ToLower();
                    if (wordProdLower == wordSearchLower) // se alguma  palavra que está no nome for igual a palavra que está na pesquisa, então retornará verdadeiro
                    {
                        return true;
                    }
                }

            }
            return false;

        }
        #endregion
        #region Get produto pela categoria
        public async Task<IEnumerable<Produto>> GetByCategoria(int categoriaId)
        {
            var produto = await _context.Produtos.Where(p => p.IdCategoria == categoriaId).ToListAsync();
            return produto;
        }
        #endregion
        #region Get Produto Pela Categoria Paginação
        public IQueryable<Produto> GetProdutosCategoriaPagination(int categoriaId)
        {
            var produto = _context.Produtos.Where(p => p.IdCategoria == categoriaId);
            return produto;
        }
        #endregion
        #region Atualizar quantidade de produto
        public async Task<IActionResult> PatchQnt(Produto produto)
        {
            var dbProd = await _context.Produtos.FirstOrDefaultAsync(p => p.Cod_Produto == produto.Cod_Produto);
            if (dbProd is not null)
            {
                dbProd.EmEstoque++;
                _context.Produtos.Update(dbProd);
                await _context.SaveChangesAsync();
                return new OkObjectResult(produto);
            }
            else
            {
                return new NotFoundObjectResult(dbProd);
            }
        }
        #endregion
    }
}
