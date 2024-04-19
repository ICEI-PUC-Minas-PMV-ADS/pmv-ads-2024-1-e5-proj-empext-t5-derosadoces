using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Controllers
{

    [AllowAnonymous]
    public class CadastroController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public CadastroController(UserManager<IdentityUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        #endregion
        #region Cadastrar
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        #endregion
        #region Cadastrar (HTTPPOST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(CadastroViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = usuario._Cliente.NomeUsuario,
                    NormalizedUserName = usuario._Cliente.NomeUsuario.ToUpper(),
                    Email = usuario._Cliente.Email,
                    NormalizedEmail = usuario._Cliente.Email.ToUpper(),
                    PhoneNumber = usuario._Cliente.Telefone

                };

                var result = await _userManager.CreateAsync(user, usuario._Cliente.Senha);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");


                    var cliente = new Cliente
                    {
                        Id_User = user.Id,
                        Nome = usuario._Cliente.Nome,
                        NomeUsuario = usuario._Cliente.NomeUsuario,
                        Telefone = usuario._Cliente.Telefone,
                        CPF = usuario._Cliente.CPF,
                        Email = usuario._Cliente.Email,
                        DateNasc = usuario._Cliente.DateNasc,
                        Senha = user.PasswordHash,
                       
                    };
                    var endereco = new Endereco{
                        Id_User = user.Id,
                        Logradouro = usuario._Endereco.Logradouro,
                        Cidade = usuario._Endereco.Cidade,
                        UF = usuario._Endereco.UF,
                        CEP = usuario._Endereco.CEP,
                        Complemento = usuario._Endereco.Complemento,
                        Numero = usuario._Endereco.Numero,
                        Bairro = usuario._Endereco.Bairro,
                        Rua = usuario._Endereco.Rua,
                    };
                    cliente._Enderecos.Add(endereco);

                    _context.Enderecos.Add(endereco);
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result.ToString());
                }
            }
            return View(usuario);
        }
        #endregion
    }
}
