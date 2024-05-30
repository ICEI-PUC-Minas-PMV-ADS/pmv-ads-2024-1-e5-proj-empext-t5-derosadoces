using DeRosaWebApp.Models;

namespace DeRosaWebApp.BusinessRules.Interfaces
{
    public interface IPedidoRules
    {
        void VerificaComemorativosSeteDiasAntecedencia(List<Produto> produtos, DateTime dataEntrega);
        void VerificaCidade(string cidade);
    }
}
