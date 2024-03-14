using Azure.Identity;
using DeRosaWebApp.Context;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;


        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }   

        public IActionResult Login(string returnUrl)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel()
            {
                ReturnUrl = returnUrl,
            };
            return View(usuarioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(usuarioViewModel.Usuario);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, usuarioViewModel.Senha, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            ModelState.AddModelError("", "Nome ou senha não coincidem!");
            return View(usuarioViewModel);

        }


        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userId = user.Id;


            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id_User == userId);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteEditViewModel = new ClienteViewModel
            {
                Nome = cliente.Nome,
                NomeUsuario = cliente.NomeUsuario,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                DateNasc = cliente.DateNasc
            };

            return View(clienteEditViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClienteViewModel clienteEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = clienteEditViewModel.NomeUsuario;
                user.NormalizedUserName = clienteEditViewModel.NomeUsuario.ToUpper();
                user.Email = clienteEditViewModel.Email;
                user.NormalizedEmail = clienteEditViewModel.Email.ToUpper();
                user.PhoneNumber = clienteEditViewModel.Telefone;


                var userId = user.Id;

                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id_User == userId);

                if (cliente == null)
                {
                    return NotFound();
                }

                cliente.Nome = clienteEditViewModel.Nome;
                cliente.NomeUsuario = clienteEditViewModel.NomeUsuario;
                cliente.Email = clienteEditViewModel.Email;
                cliente.Telefone = clienteEditViewModel.Telefone;
                cliente.DateNasc = clienteEditViewModel.DateNasc;

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(clienteEditViewModel);
        }


        public IActionResult AccessDenied()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
