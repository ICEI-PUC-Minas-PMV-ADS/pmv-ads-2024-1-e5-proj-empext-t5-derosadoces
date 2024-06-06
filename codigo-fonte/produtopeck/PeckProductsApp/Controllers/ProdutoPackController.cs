using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PeckProductsApp.Data;
using PeckProductsApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PeckProductsApp.Controllers
{
    public class ProdutoPackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProdutoPackController> _logger;

        public ProdutoPackController(ApplicationDbContext context, ILogger<ProdutoPackController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Listar todos os produtos pack
        public async Task<IActionResult> Index()
        {
            var produtoPacks = await _context.ProdutoPacks.Include(p => p.Sabores).ToListAsync();
            return View(produtoPacks);
        }

        // Tela de cadastro do produto pack
        public IActionResult Create()
        {
            return View(new ProdutoPack());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoPack produtoPack, string[] Sabores)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Iniciando criação de ProdutoPack");

                // Salvar o ProdutoPack primeiro
                _context.Add(produtoPack);
                await _context.SaveChangesAsync();

                _logger.LogInformation("ProdutoPack salvo com ID {Id}", produtoPack.Id);

                // Adicionar sabores vinculados ao ProdutoPack
                foreach (var saborNome in Sabores)
                {
                    if (!string.IsNullOrEmpty(saborNome))
                    {
                        var sabor = new Sabor { Nome = saborNome, ProdutoPackId = produtoPack.Id };
                        _context.Sabores.Add(sabor);
                    }
                }
                await _context.SaveChangesAsync();

                _logger.LogInformation("Sabores salvos para ProdutoPack com ID {Id}", produtoPack.Id);

                return RedirectToAction(nameof(Index));
            }

            _logger.LogWarning("ModelState inválido ao tentar criar ProdutoPack");
            return View(produtoPack);
        }

        // Tela de edição do produto pack
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoPack = await _context.ProdutoPacks.Include(p => p.Sabores).FirstOrDefaultAsync(p => p.Id == id);
            if (produtoPack == null)
            {
                return NotFound();
            }

            return View(produtoPack);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoPack produtoPack, string[] Sabores)
        {
            if (id != produtoPack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoPack);
                    await _context.SaveChangesAsync();

                    var existingSabores = _context.Sabores.Where(s => s.ProdutoPackId == id);
                    _context.Sabores.RemoveRange(existingSabores);
                    await _context.SaveChangesAsync();

                    foreach (var saborNome in Sabores)
                    {
                        if (!string.IsNullOrEmpty(saborNome))
                        {
                            var sabor = new Sabor { Nome = saborNome, ProdutoPackId = produtoPack.Id };
                            _context.Sabores.Add(sabor);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoPackExists(produtoPack.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produtoPack);
        }

        // Deletar produto pack
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoPack = await _context.ProdutoPacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoPack == null)
            {
                return NotFound();
            }

            return View(produtoPack);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoPack = await _context.ProdutoPacks.FindAsync(id);
            var sabores = _context.Sabores.Where(s => s.ProdutoPackId == id);
            _context.Sabores.RemoveRange(sabores);
            _context.ProdutoPacks.Remove(produtoPack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoPackExists(int id)
        {
            return _context.ProdutoPacks.Any(e => e.Id == id);
        }
    }
}
