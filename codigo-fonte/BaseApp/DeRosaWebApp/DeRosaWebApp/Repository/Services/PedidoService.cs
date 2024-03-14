using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Repository.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _context;
        private readonly Carrinho _carrinho;
        public PedidoService(AppDbContext context, Carrinho carrinho)
        {
            _context = context;
            _carrinho = carrinho;
        }
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            var pedidos = await _context.Pedidos.ToListAsync();
            return pedidos;
        }
        public async Task<ActionResult<Pedido>> Delete(int id)
        {
            var pedido = await GetById(id);
            if (pedido is not null)
            {
                _context.Pedidos.Remove(pedido.Value);
                await _context.SaveChangesAsync();
                return new OkObjectResult("O pedido foi excluido com sucesso!");
            }
            return new NotFoundObjectResult("O pedido não foi encontrado!");
        }
        public async Task<ActionResult<Pedido>> GetById(int id)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Cod_Pedido == id);
            if (pedido is not null)
            {
                return pedido;
            }
            else
            {
                return new NotFoundObjectResult($"O pedido não foi encontrado, verifique o ID:{id}");
            }
        }
        public async Task<ActionResult<Pedido>> Update(int id, Pedido pedido)
        {
            if (pedido is not null)
            {
                var pedidoExist = await _context.Pedidos.FirstOrDefaultAsync(p => p.Cod_Pedido == id);
                if (pedidoExist is not null)
                {

                    pedidoExist.Nome = pedido.Nome;
                    pedidoExist.DataPedido = pedido.DataPedido;
                    pedidoExist.Rua = pedido.Rua;
                    pedidoExist.Numero = pedido.Numero;
                    pedidoExist.Telefone = pedido.Telefone;
                    pedidoExist.TotalItensPedido = pedido.TotalItensPedido;
                    pedidoExist.TotalPedido = pedido.TotalPedido;
                    pedidoExist.Entregue = pedido.Entregue;
                    pedidoExist.Concluido = pedido.Concluido;

                    _context.Pedidos.Update(pedidoExist);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult($"O pedido ID:{pedido.Cod_Pedido} foi atualizado com sucesso! Data de alteração:{DateTime.Now}");
                }
                return new NotFoundObjectResult("Pedido não encontrado!");
            }
            return new BadRequestObjectResult("O Pedido é nulo!");
        }
        public async Task<ActionResult> CriarPedido(Pedido pedido)
        {
            if (pedido is not null)
            {
                await _context.Pedidos.AddAsync(pedido);
                await _context.SaveChangesAsync();
                var carrinhoItems = _carrinho.ListItemCarrinho;
                List<string> strList = new List<string>();
                string result = "";
                foreach (var produto in carrinhoItems)
                {
                    string produtoIdString = produto.Produto.Cod_Produto.ToString();
                    strList.Add(produtoIdString);
                }
                foreach (string produtoIdString in strList) // adicionando todos os ids em uma string, para futuramente usar split(",") e obter todos os ids
                {
                    result += produtoIdString + ",";
                }
                result = result.TrimEnd(',');


                foreach (var item in carrinhoItems)
                {

                    PedidoDetalhe pedidoDetalhe = new PedidoDetalhe
                    {
                        Cod_PedidoDetalhe = pedido.Cod_Pedido,
                        Cod_Produto = item.Produto.Cod_Produto,
                        Quantidade = item.QntProduto,
                        Preco = (decimal)(item.Produto.Preco * item.QntProduto),
                        Produto = item.Produto,
                        Conjunto_Pedidos = result,
                    };
                    _context.PedidoDetalhes.Add(pedidoDetalhe);
                }
                await _context.SaveChangesAsync();
                return new OkObjectResult("Pedido criado com sucesso!");
            }
            return new NotFoundObjectResult("O Pedido é nulo!");
        }

        public async Task<ActionResult> VerificarPedido(int id)
        {
            throw new NotImplementedException();
        }
        public PedidoDetalhe DetalhePedido(int id)
        {
            var detalhe = _context.PedidoDetalhes.FirstOrDefault(p => p.Cod_Pedido == id);
            if (detalhe is null)
            {
                PedidoDetalhe pedidoDetalheNull = new PedidoDetalhe();
                return pedidoDetalheNull;
            }
            return detalhe;
        }
        public List<Produto> ProdutosPedido(int id)
        {
            var produtosPedido = _context.Produtos.Where(p => p.Cod_Produto == id).ToList();
            return produtosPedido;
        }
    }
}
