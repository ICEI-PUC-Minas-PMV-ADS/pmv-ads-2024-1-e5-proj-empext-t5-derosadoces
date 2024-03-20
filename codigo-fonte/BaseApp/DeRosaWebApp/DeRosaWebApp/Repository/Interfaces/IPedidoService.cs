using DeRosaWebApp.Models;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Repository.Interfaces
{
    public interface IPedidoService
    {
        Task<ActionResult> CriarPedido(Pedido pedido, string user_id);
        Task<ActionResult> VerificarPedido(int id);
        PedidoDetalhe DetalhePedido(int id);
        List<PedidoDetalhe> DetalhePedidoList(int id);
        List<Produto> ProdutosPedido(int id);
        Task<ActionResult<Pedido>> Update(int id, Pedido pedido);
        Task<ActionResult<Pedido>> GetById(int id);
        Task<ActionResult<Pedido>> Delete(int id);
        Task<ActionResult<IEnumerable<Pedido>>> GetAll();
        Task<MeusPedidosViewModel> GetMeusPedidos(string user_id);
        Task<IEnumerable<Produto>> GetMeusProdutos(int cod_pedido);
    }
}
