namespace DeRosaWebApp.BusinessRules.Interfaces
{
    public interface IProductRules
    {
        Task VerificaQuantidadeEmEstoque(int quantidadeRecebida, int cod_produto);
    }
}
