
using DeRosaWebApp.Context;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DeRosaWebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IClienteService _clienteService;


        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IClienteService clienteService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _clienteService = clienteService;
        }
        #endregion
        #region Login

        public IActionResult Login(string returnUrl)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel()
            {
                ReturnUrl = returnUrl,
            };
            return View(usuarioViewModel);
        }
        #endregion
        #region Login (HTTPPOST)

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
        #endregion
        #region Edit usuário

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userId = user.Id;


            var cliente = await _clienteService.GetClienteByIdAsync(userId);

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
        #endregion
        #region Edit usuário (HTTPPOST)

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

                var cliente = await _clienteService.GetClienteByIdAsync(userId);

                if (cliente == null)
                {
                    return NotFound();
                }

                cliente.Nome = clienteEditViewModel.Nome;
                cliente.NomeUsuario = clienteEditViewModel.NomeUsuario;
                cliente.Email = clienteEditViewModel.Email;
                cliente.Telefone = clienteEditViewModel.Telefone;
                cliente.DateNasc = clienteEditViewModel.DateNasc;

                await _clienteService.UpdateClienteAsync(cliente);

                return RedirectToAction("Index", "Home");
            }
            return View(clienteEditViewModel);
        }
        #endregion
        #region AccessDenied

        public IActionResult AccessDenied()
        {

            return View();
        }
        #endregion
        #region Logout

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
