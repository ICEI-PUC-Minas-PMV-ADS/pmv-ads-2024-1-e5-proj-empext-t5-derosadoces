using DeRosaWebApp.Models.Management;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            return View("AlterarTituloSobre",manageSobreAtual);
        }
        [HttpPost]
        [Route("{controller}/AlterarTituloSobre")]
        public async Task<IActionResult> AlterarTituloSobre(ManagementSobre managementTituloSobre)
        {
            if (!string.IsNullOrEmpty(managementTituloSobre.TituloSobre) && ModelState.IsValid)
            {
                await _manageSite.AlterarTituloSobre(managementTituloSobre);
                return View("Index");
            }
            ModelState.AddModelError("Erro", "Verifique se os campos foram preenchidos corretamente e tente novamente!");
            return View("AlterarTituloSobre",managementTituloSobre);
        }
        [HttpGet]
        [Route("{controller}/AlterarTextoSobre")]
        public async Task<IActionResult> AlterarTextoSobre()
        {
            var managementsSobre = await _manageSite.GetManagementSobre();

            return View("AlterarTextoSobre",managementsSobre);
        }
        [HttpPost]
        [Route("{controller}/AlterarTextoSobre")]
        public async Task<IActionResult> AlterarTextoSobre(ManagementSobre managementSobreTexto)
        {
            await _manageSite.AlterarTextoSobre(managementSobreTexto);
            return View("Index");
        }
        [HttpGet]
        [Route("{controller}/AlterarTituloSemana")]

        public async Task<IActionResult> AlterarTituloSemana()
        {
            var tituloAtual = await _manageSite.GetManagementsHome();
            return View("AlterarTituloSemana",tituloAtual);
        }
        [HttpPost]
        [Route("{controller}/AlterarTituloSemana")]
        public async Task<IActionResult> AlterarTituloSemana(ManagementHome managementtitulo)
        {
            await _manageSite.AlterarTituloDaSemana(managementtitulo);
            return View("Index");
        }
    }
}
