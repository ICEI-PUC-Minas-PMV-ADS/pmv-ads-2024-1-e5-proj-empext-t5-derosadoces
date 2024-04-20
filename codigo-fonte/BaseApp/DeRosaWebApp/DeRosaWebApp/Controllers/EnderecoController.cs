using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.Repository.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IClienteService _clienteService;
        public EnderecoController(IEnderecoService enderecoService, UserManager<IdentityUser> userManager, IClienteService clienteService)
        {
            _enderecoService = enderecoService;
            _userManager = userManager;
            _clienteService = clienteService;
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
            e.Rua = endereco.Rua;

            await _enderecoService.Update(e);
            ViewBag.SucessoEdit = "Endereco atualizado com sucesso!";
            await Task.Delay(1000);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> EscolhaEndereco()
        {
            var user_id = _userManager.GetUserId(User);
            List<Endereco> enderecos = await _enderecoService.GetListaEnderecoUsuario(user_id);
            return View(enderecos);
        }
        [HttpPost]
        public async Task<IActionResult> EscolhaEndereco(int enderecoId)
        {
            var user_id = _userManager.GetUserId(User);

            if (User.IsInRole("Admin"))
            {
                Cliente adminToClient = new Cliente()
                {
                    Nome = "admin",
                    NomeUsuario = "admin@localhost",
                    Email = "admin@gmail.com",
                    Id_User = user_id,
                    IdEndereco = enderecoId,
                    CPF = "00000000000",
                    Telefone = "9999999999",
                    Senha = "Hash",
                    DateNasc = DateTime.Now.Date,
                };
               await _clienteService.Add(adminToClient);
            }
            var cliente = await _clienteService.GetClienteByUserId(user_id);
            await _clienteService.UpdateOnlyEnderecoId(enderecoId, cliente.Cod_Cliente);

            return RedirectToAction("Checkout", "Pedido");
        }
        [HttpGet]
        public IActionResult AdicionarEndereco(string origem)
        {
            return View(origem);
        }
        [HttpPost]
        public async Task<IActionResult> AdicionarEndereco(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                var user_id = _userManager.GetUserId(User);
                endereco.Id_User = user_id;
                bool isSucess = await _enderecoService.Add(endereco);
                if (isSucess)
                {
                    ViewBag.Sucesso = "Endereço adicionado com sucesso!";

                    return RedirectToAction("Index", "Carrinho");

                }
                else
                {
                    ModelState.AddModelError("Erro", "Ocorreu algum erro ao adicionar o endereço!");
                    return View(endereco);
                }
            }
            else
            {
                ModelState.AddModelError("ErroDados", "Verifique os campos e tente novamente!");
                return View(endereco);
            }
        }

    }
}
