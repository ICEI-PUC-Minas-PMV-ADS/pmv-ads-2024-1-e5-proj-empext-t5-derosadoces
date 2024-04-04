using NUnit.Framework;
using System.Threading.Tasks;
using DeRosaWebApp.Controllers;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Identity;
using DeRosaWebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Linq.Expressions;
using System;
using DeRosaWebApp.Repository.Interfaces;

namespace DeRosaWebApp.Tests.Controllers
{
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<IEmailService> _mockEmailService;
        //private Mock<RoleManager<IdentityRole> _mockRoleManager;

        [SetUp]
        public void Setup()
        {
           // _mockUserManager = new Mock<UserManager<IdentityUser>>();
            //_mockEmailService = new Mock<IEmailService>();
          //  _mockRoleManager = new Mock<RoleManager<IdentityRole>>(); 
          //  _controller = new AccountController(
         //       null, null, null, _mockUserManager.Object, _mockEmailService.Object, null, _mockRoleManager.Object); // <-- Passe o roleManager aqui
        }

        [Test]
        public async Task EsqueciSenha_UsuarioExistente_RetornaViewEmailRedefinicaoEnviado()
        {
            // Arrange
            var email = "test@example.com";
            var dados = new EsqueciSenhaViewModel { Email = email };
            _mockUserManager.Setup(u => u.Users.AnyAsync(It.IsAny<Expression<Func<IdentityUser, bool>>>(), CancellationToken.None))
     .ReturnsAsync(true); // Ou qualquer valor que seja apropriado para o seu teste
            
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
            _mockUserManager.Setup(u => u.Users.AnyAsync(u => u.NormalizedEmail == email.ToUpper().Trim(), CancellationToken.None))
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