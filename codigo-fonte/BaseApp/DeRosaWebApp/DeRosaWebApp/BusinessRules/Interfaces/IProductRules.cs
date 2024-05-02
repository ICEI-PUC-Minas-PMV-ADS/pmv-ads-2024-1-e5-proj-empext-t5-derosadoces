namespace DeRosaWebApp.BusinessRules.Interfaces
{
    public interface IProductRules
    {
        Task VerificaQuantidadeEmEstoque(int quantidadeRecebida, int cod_produto);
        Task VerificaQuantidadeEmEstoqueAgendamento(int quantidadeRecebida, int cod_produto);
    }
}
