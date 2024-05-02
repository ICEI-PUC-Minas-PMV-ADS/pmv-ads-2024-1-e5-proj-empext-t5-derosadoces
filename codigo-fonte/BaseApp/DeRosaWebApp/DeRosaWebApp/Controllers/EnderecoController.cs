using DeRosaWebApp.BusinessRules.Validations;
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

            await _enderecoService.Update(e);
            ViewBag.SucessoEdit = "Endereco atualizado com sucesso!";
            await Task.Delay(1000);
            string getSession = HttpContext.Session.GetString("EditEndereco");
            if (string.Equals(getSession, "EscolhaEndereco"))
            {
                return RedirectToAction("EscolhaEndereco", "Endereco");
            }
            else if (string.Equals(getSession, "MinhaConta"))
            {
                return RedirectToAction("Edit", "Account");
            }
            return View(endereco);
        }
        [HttpGet]
        public async Task<IActionResult> EscolhaEndereco()
        {
            var user_id = _userManager.GetUserId(User);
            List<Endereco> enderecos = await _enderecoService.GetListaEnderecoUsuario(user_id);
            
            HttpContext.Session.SetString("AddEndereco", "EscolhaEndereco");
            HttpContext.Session.SetString("EditEndereco", "EscolhaEndereco");
            


            return View(enderecos);
        }
        [HttpPost]
        public async Task<IActionResult> EscolhaEndereco(int selectedAddressId)
        {
            try
            {
                if (selectedAddressId == 0)
                {
                    throw new DeRosaExceptionValidation("Selecione um endereço!");
                }
                var user_id = _userManager.GetUserId(User);

                if (User.IsInRole("Admin"))
                {
                    Cliente adminToClient = new Cliente()
                    {
                        Nome = "admin",
                        NomeUsuario = "admin@localhost",
                        Email = "admin@gmail.com",
                        Id_User = user_id,
                        IdEndereco = selectedAddressId,
                        CPF = "00000000000",
                        Telefone = "9999999999",
                        Senha = "Hash",
                        DateNasc = DateTime.Now.Date,
                    };
                    await _clienteService.Add(adminToClient);
                }
                var cliente = await _clienteService.GetClienteByUserId(user_id);
                await _clienteService.UpdateOnlyEnderecoId(selectedAddressId, cliente.Cod_Cliente);
                
                return RedirectToAction("Checkout", "Pedido");
            }
            catch (DeRosaExceptionValidation e)
            {
                TempData["Erro"] = e.Message;
                return RedirectToAction("EscolhaEndereco", "Endereco");
            }

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
                    string getSession = HttpContext.Session.GetString("AddEndereco");
                    if (string.Equals(getSession, "MinhaConta"))
                    {
                        return RedirectToAction("Edit", "Account");
                    }
                    else if (string.Equals(getSession, "EscolhaEndereco"))
                    {
                        return RedirectToAction("EscolhaEndereco", "Endereco");
                    }
                    return View(endereco);

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
