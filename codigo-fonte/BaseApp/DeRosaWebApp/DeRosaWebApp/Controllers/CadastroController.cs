using DeRosaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Controllers
{

    [AllowAnonymous]
    public class CadastroController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CadastroController(UserManager<IdentityUser> userManager)
        {

            _userManager = userManager;
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
                    UserName = usuario.Nome,
                    NormalizedUserName = usuario.Nome.ToUpper(),
                    Email = usuario.Email,
                    NormalizedEmail = usuario.Email.ToUpper(),
                    PhoneNumber = usuario.Telefone

                };

                var result = await _userManager.CreateAsync(user, usuario.Senha);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.ToString());
                }
            }
            return View(usuario);

        }
    }
}
