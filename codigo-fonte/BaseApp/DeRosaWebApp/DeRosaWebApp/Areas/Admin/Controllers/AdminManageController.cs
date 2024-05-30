using DeRosaWebApp.Models.Management;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminManageController : Controller
    {
        private readonly IManageSite _manageSite;

        public AdminManageController(IManageSite manageSite)
        {
            _manageSite = manageSite;
        }

        [HttpGet]
        [Route("{controller}")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("{controller}/AlterarTituloSobre")]
        public async Task<IActionResult> AlterarTituloSobre()
        {
            var manageSobreAtual = await _manageSite.GetManagementSobre();
            return View(manageSobreAtual);
        }
        [HttpPost]
        [Route("{controller}/AlterarTituloSobre")]
        public async Task<IActionResult> AlterarTituloSobre(string tituloSobre)
        {
            if (!string.IsNullOrEmpty(tituloSobre) && ModelState.IsValid)
            {
                await _manageSite.AlterarTituloSobre(tituloSobre);
                return View("Index");
            }
            ModelState.AddModelError("Erro", "Verifique se os campos foram preenchidos corretamente e tente novamente!");
            return View(tituloSobre);
        }
        [HttpGet]
        [Route("{controller}/AlterarTextoSobre")]
        public async Task<IActionResult> AlterarTextoSobre()
        {
            var managementsSobre = await _manageSite.GetManagementSobre();

            return View("AlterarTextoSobre",managementsSobre.TextoSobre);
        }
        [HttpPost]
        [Route("{controller}/AlterarTextoSobre")]
        public async Task<IActionResult> AlterarTextoSobre(string textoSobre)
        {
            await _manageSite.AlterarTextoSobre(textoSobre);
            return View("Index");
        }
        [HttpGet]
        [Route("{controller}/AlterarTituloSemana")]

        public async Task<IActionResult> AlterarTituloSemana()
        {
            var tituloAtual = await _manageSite.GetManagementsHome();
            return View("AlterarTituloSemana",tituloAtual.TituloSemana);
        }
        [HttpPost]
        [Route("{controller}/AlterarTituloSemana")]
        public async Task<IActionResult> AlterarTituloSemana(string titulo)
        {
            await _manageSite.AlterarTituloDaSemana(titulo);
            return View("Index");
        }
    }
}
