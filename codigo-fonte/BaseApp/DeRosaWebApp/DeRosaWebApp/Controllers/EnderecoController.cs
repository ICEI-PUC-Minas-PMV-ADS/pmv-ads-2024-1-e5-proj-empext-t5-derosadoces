using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }
        [HttpGet]
        public async Task<IActionResult> EditEndereco(int enderecoId)
        {
            Endereco e = await _enderecoService.GetEnderecoById(enderecoId);
            return View(e);
        }
        [HttpPost]
        public async Task<IActionResult> EditEndereco(Endereco endereco)
        {
            Endereco e = await _enderecoService.GetEnderecoById(endereco.Id);

            e.CEP = endereco.CEP;
            e.UF = endereco.UF;
            e.Cidade = endereco.Cidade;
            e.Id_User = endereco.Id_User;
            e.Logradouro = endereco.Logradouro;
            e.Numero = endereco.Numero;
            e.Complemento = endereco.Complemento;
            e.Bairro = endereco.Bairro;
            e.Id_User = endereco.Id_User;

            await _enderecoService.Update(e);
            return RedirectToAction("Edit", "Account");
        }
    }
}
