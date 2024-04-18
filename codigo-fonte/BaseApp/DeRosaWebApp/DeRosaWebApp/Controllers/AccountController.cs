
using DeRosaWebApp.Context;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DeRosaWebApp.Extensions;
using DeRosaWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace DeRosaWebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IClienteService _clienteService;
        private readonly UserManager<IdentityUser> _userManagerCliente;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManagerCliente;
        private readonly IEmailService _emailService;
        private readonly IEnderecoService _enderecoService;


        public AccountController(
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager,
    IClienteService clienteService,
    UserManager<IdentityUser> userManagerCliente,
    IEmailService emailService,
    SignInManager<IdentityUser> signInManagerCliente,
    IEnderecoService enderecoService,
    RoleManager<IdentityRole> roleManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _clienteService = clienteService;
            _userManagerCliente = userManagerCliente;
            _signInManagerCliente = signInManagerCliente;
            _roleManager = roleManager; 
            _emailService = emailService;
            _enderecoService = enderecoService;
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

            var enderecos = await _enderecoService.GetListaEnderecoUsuario(userId);

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
                DateNasc = cliente.DateNasc,
                _Enderecos = enderecos,
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

                foreach (Endereco e in clienteEditViewModel._Enderecos)
                {
                    var endereco = await _enderecoService.GetEnderecoById(e.Id);
                    endereco.Logradouro = e.Logradouro;
                    endereco.Numero = e.Numero;
                    endereco.Complemento = e.Complemento;
                    endereco.Bairro = e.Bairro;
                    endereco.Cidade = e.Cidade;
                    endereco.UF = e.UF;
                    endereco.CEP = e.CEP;

                    await _enderecoService.Update(endereco);
                }

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
            TempData["LogoutMessage"] = "Sessao finalizada";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        [HttpGet]
        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EsqueciSenha([FromForm] EsqueciSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.Users.AsNoTracking().Any(u => u.NormalizedEmail == dados.Email.ToUpper().Trim()))
                {
                    var usuario = await _userManagerCliente.FindByEmailAsync(dados.Email);
                    var token = await _userManagerCliente.GeneratePasswordResetTokenAsync(usuario);
                    var urlConfirmacao = Url.Action(nameof(RedefinirSenha), "UserName", new { token }, Request.Scheme);
                    var mensagem = new StringBuilder();
                    mensagem.Append($"<p>Olá, {usuario.UserName}.</p>");
                    mensagem.Append("<p>Houve uma solicitação de redefinição de senha para seu usuário em nosso site. Se não foi você que fez a solicitação, ignore essa mensagem. Caso tenha sido você, clique no link abaixo para criar sua nova senha:</p>");
                    mensagem.Append($"<p><a href='{urlConfirmacao}'>Redefinir Senha</a></p>");
                    mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte</p>");
                    await _emailService.SendEmailAsync(usuario.Email,
                        "Redefinição de Senha", "", mensagem.ToString());
                    return View(nameof(EmailRedefinicaoEnviado));
                }
                else
                {
                    this.MostrarMensagem(
                            $"Usuário/e-mail <b>{dados.Email}</b> não encontrado.");
                    return View();
                }
            }
            else
            {
                return View(dados);
            }
        }

        public IActionResult EmailRedefinicaoEnviado()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RedefinirSenha(string token)
        {
            var modelo = new RedefinirSenhaViewModel();
            modelo.Token = token;
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedefinirSenha([FromForm] RedefinirSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManagerCliente.FindByEmailAsync(dados.Email);
                var resultado = await _userManagerCliente.ResetPasswordAsync(
                    usuario, dados.Token, dados.NovaSenha);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem(
                       $"Senha redefinida com sucesso! Agora você já pode fazer login com a nova senha.");
                    return View(nameof(Login));
                }
                else
                {
                    this.MostrarMensagem(
                        $"Não foi possível redefinir a senha. Verifique se preencheu a senha corretamente. Se o problema persistir, entre em contato com o suporte.");
                    return View(dados);
                }
            }
            else
            {
                return View(dados);
            }
        }

        [HttpGet, Authorize]
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha([FromForm] AlterarSenhaViewModel dados)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManagerCliente.FindByEmailAsync(HttpContext.User.Identity.Name);
                var resultado = await _userManagerCliente.ChangePasswordAsync(usuario, dados.SenhaAtual, dados.NovaSenha);
                if (resultado.Succeeded)
                {
                    this.MostrarMensagem(
                        $"Sua senha foi alterada com sucesso. Identifique-se usando a nova senha.");
                    await _signInManagerCliente.SignOutAsync();
                    return RedirectToAction(nameof(Login), "Usuario");
                }
                else
                {
                    this.MostrarMensagem(
                        $"Não foi possível alterar sua senha. Confira os dados informados e tente novamente.");
                    return View(dados);
                }
            }
            else
            {
                return View(dados);
            }
        }

        private async Task EnviarLinkConfirmacaoEmailAsync(Cliente usuario)
        {
            var user = await _userManager.FindByNameAsync(usuario.Nome);
            var token = await _userManagerCliente.GenerateEmailConfirmationTokenAsync(user);
            var urlConfirmacao = Url.Action("ConfirmarEmail",
                "Usuario", new { email = usuario.Email, token }, Request.Scheme);
            var mensagem = new StringBuilder();
            mensagem.Append($"<p>Olá, {usuario.Nome}.</p>");
            mensagem.Append("<p>Recebemos seu cadastro em nosso sistema. Para concluir o processo de cadastro, clique no link a seguir:</p>");
            mensagem.Append($"<p><a href='{urlConfirmacao}'>Confirmar Cadastro</a></p>");
            mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte</p>");
            await _emailService.SendEmailAsync(usuario.Email,
                "Confirmação de Cadastro", "", mensagem.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarEmail(string email, string token)
        {
            var usuario = await _userManagerCliente.FindByEmailAsync(email);
            if (usuario == null)
            {
                this.MostrarMensagem("Não foi possível confirmar o e-mail. Usuário não encontrado", true);
            }
            var resultado = await _userManagerCliente.ConfirmEmailAsync(usuario, token);
            if (resultado.Succeeded)
            {
                this.MostrarMensagem("E-mail confirmado com sucesso! Agora você já está liberado para fazer o login.");
            }
            else
            {
                this.MostrarMensagem("Não foi possível validar seu e-mail. Tente novamente em alguns minutos. Se o problema persistir, entre em contato com o suporte.", true);
            }
            return View(nameof(Login));
        }
    }
}
