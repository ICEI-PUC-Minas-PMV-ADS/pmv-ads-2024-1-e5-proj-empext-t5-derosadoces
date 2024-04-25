using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.BusinessRules.Validations;
using DeRosaWebApp.Repository.Interfaces;

namespace DeRosaWebApp.BusinessRules.Services
{
    public class ProductRules : IProductRules
    {
        // Não use AppDbContext aqui, por questões de segurança

        private readonly IProductService _productService;
        public ProductRules(IProductService productService)
        {
            _productService = productService;
        }

        public async Task VerificaQuantidadeEmEstoque(int quantidadeRecebida, int cod_produto)
        {
            int quantidadeEmEstoque = await _productService.QuantidadeEmEstoque(cod_produto);
            DeRosaExceptionValidation.When(quantidadeRecebida > quantidadeEmEstoque,
                   $"Quantidade não disponível no estoque, por favor, diminua a quantidade.");
            DeRosaExceptionValidation.When(quantidadeEmEstoque == 0,
                  $"Produto indisponível nessa semana!");
            DeRosaExceptionValidation.When(quantidadeEmEstoque - quantidadeRecebida  < 0,
                  $"Produto indisponível nessa semana!");
        }
    }
}
