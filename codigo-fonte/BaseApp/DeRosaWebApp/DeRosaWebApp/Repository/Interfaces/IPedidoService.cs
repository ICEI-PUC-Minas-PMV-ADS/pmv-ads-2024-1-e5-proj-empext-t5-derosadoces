using DeRosaWebApp.Models;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IPedidoService
    {
        Task<ActionResult> CriarPedido(Pedido pedido, string user_id);
        Task<ActionResult<Pedido>> VerificarPedidosExpirados();
        PedidoDetalhe DetalhePedido(int id);
        Task<List<PedidoDetalhe>> DetalhePedidoList(int id);
        List<Produto> ProdutosPedido(int id);
        Task<ActionResult<Pedido>> Update(int id, Pedido pedido);
        Task<ActionResult<Pedido>> GetById(int id);
        Task<ActionResult<Pedido>> Delete(int id);
        Task<ActionResult<IEnumerable<Pedido>>> GetAll();
        Task<MeusPedidosViewModel> GetMeusPedidos(string user_id);
        Task<IEnumerable<Produto>> GetMeusProdutos(int cod_pedido);
        Task<ActionResult<Pedido>> UpdatePayment(int cod_pedido, bool pago);
        Task<ActionResult<Pedido>> Remove(Pedido pedido);
      
        
    }
}
