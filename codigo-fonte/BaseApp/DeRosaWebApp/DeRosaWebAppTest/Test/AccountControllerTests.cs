using NUnit.Framework;
using System.Threading.Tasks;
using DeRosaWebApp.Controllers;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using DeRosaWebApp.Services;
using Microsoft.AspNetCore.Identity;
using DeRosaWebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace DeRosaWebApp.Tests.Controllers
{
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<UserManager<Cliente>> _mockUserManager;
        private Mock<IEmailService> _mockEmailService;

        [SetUp]
        public void Setup()
        {
            _mockUserManager = new Mock<UserManager<Cliente>>();
            _mockEmailService = new Mock<IEmailService>();
            _controller = new AccountController(
                null, null, _mockUserManager.Object, _mockEmailService.Object, null, null);
        }

        [Test]
        public async Task EsqueciSenha_UsuarioExistente_RetornaViewEmailRedefinicaoEnviado()
        {
            // Arrange
            var email = "test@example.com";
            var dados = new EsqueciSenhaViewModel { Email = email };
            _mockUserManager.Setup(u => u.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpper().Trim()))
                .ReturnsAsync(true);
            _mockUserManager.Setup(u => u.FindByEmailAsync(email))
                .ReturnsAsync(new Cliente { Email = email });

            // Act
            var result = await _controller.EsqueciSenha(dados) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("EmailRedefinicaoEnviado"));
        }

        [Test]
        public async Task EsqueciSenha_UsuarioNaoExistente_RetornaView()
        {
            // Arrange
            var email = "test@example.com";
            var dados = new EsqueciSenhaViewModel { Email = email };
            _mockUserManager.Setup(u => u.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpper().Trim()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.EsqueciSenha(dados) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.Empty);

        }

        // Adicione mais testes para os outros métodos, se necessário

        [TearDown]
        public void TearDown()
        {
            _controller = null;
            _mockUserManager = null;
            _mockEmailService = null;
        }
    }
}