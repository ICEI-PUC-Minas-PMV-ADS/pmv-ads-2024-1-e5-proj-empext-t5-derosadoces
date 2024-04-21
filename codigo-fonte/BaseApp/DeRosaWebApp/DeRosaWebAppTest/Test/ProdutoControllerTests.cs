using DeRosaWebApp.Controllers;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DeRosaWebApp.Tests.Controllers
{
    [TestFixture]
    public class ProdutoControllerTests
    {
        private ProdutoController _produtoController;
        private Mock<IProductService> _mockProdutoService;

        [SetUp]
        public void Setup()
        {
            _mockProdutoService = new Mock<IProductService>();
            _produtoController = new ProdutoController(_mockProdutoService.Object, null);
        }

        [Test]
        public async Task ProdutoDetalhe_DeveRetornarViewComProduto()
        {
            // Arrange
            int codProduto = 1;
            var produto = new Produto
            {
                Cod_Produto = codProduto,
                Nome = "Nome do Produto",
                Preco = 9.99,
                EmEstoque = 2,
                DescricaoCurta = "Descrição",
                PrecoSecundario = 10.00M,
            };

            _mockProdutoService.Setup(x => x.GetById(codProduto)).ReturnsAsync(produto);
            _mockProdutoService.Setup(x => x.Create(It.IsAny<Produto>())).ReturnsAsync(new OkResult());

            var result = await _produtoController.ProdutoDetalhe(codProduto) as ViewResult;
            var model = result?.Model as Produto; 

            Assert.That(result, Is.Not.Null);
        }

    }
}
