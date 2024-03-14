using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Controllers
{

    [AllowAnonymous]
    public class CadastroController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public CadastroController(UserManager<IdentityUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Cliente usuario)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = usuario.NomeUsuario,
                    NormalizedUserName = usuario.NomeUsuario.ToUpper(),
                    Email = usuario.Email,
                    NormalizedEmail = usuario.Email.ToUpper(),
                    PhoneNumber = usuario.Telefone

                };

                var result = await _userManager.CreateAsync(user, usuario.Senha);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");


                    var cliente = new Cliente
                    {
                        Id_User = user.Id,
                        Nome = usuario.Nome,
                        NomeUsuario = usuario.NomeUsuario,
                        Telefone = usuario.Telefone,
                        CPF = usuario.CPF,
                        Email = usuario.Email,
                        DateNasc = usuario.DateNasc,
                        Senha = user.PasswordHash

                    };
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
    }
}
