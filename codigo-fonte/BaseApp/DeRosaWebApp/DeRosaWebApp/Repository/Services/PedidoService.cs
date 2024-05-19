using DeRosaWebApp.BusinessRules.Validations;
using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DeRosaWebApp.Repository.Services
{
    public class PedidoService : IPedidoService
    {
        #region Injeção de dependecia, Construtor e propriedades
        private readonly AppDbContext _context;
        private readonly Carrinho _carrinho;
        private readonly IProductService _productService;
        private readonly UserManager<IdentityUser> UserManager;
        public PedidoService(AppDbContext context, Carrinho carrinho, IProductService productService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _carrinho = carrinho;
            UserManager = userManager;
            _productService = productService;
        }
        #endregion



        #region Remover pedido
        public async Task<ActionResult<Pedido>> Remove(Pedido pedido)
        {
            var _pedidoExist = await _context.Pedidos.FirstOrDefaultAsync(p => p.Cod_Pedido == pedido.Cod_Pedido);
            if (_pedidoExist is not null)
            {

                var itensCarrinho = await _context.ItemCarrinhos.Where(p => p.Cod_Carrinho == _carrinho.Cod_Carrinho).ToListAsync();
                _context.RemoveRange(itensCarrinho);
                _context.Remove(_pedidoExist);
                await _context.SaveChangesAsync();
            }
            return new NotFoundObjectResult("Não foi possivel remover o pedido");
        }
        #endregion
        #region Update somente pagamento
        public async Task<ActionResult<Pedido>> UpdatePayment(int cod_pedido, bool pago)
        {
            var pedido = await _context.Pedidos.FindAsync(cod_pedido);

            if (pedido == null)
            {
                return new NotFoundObjectResult("Pedido não encontrado");
            }

            pedido.Pago = pago;
            _context.Entry(pedido).Property(x => x.Pago).IsModified = true;
            pedido.DataExpiracao.AddDays(9999);
            _context.Entry(pedido).Property(x => x.DataExpiracao).IsModified = true;
            await _context.SaveChangesAsync();

            return new OkObjectResult($"O pedido foi pago com sucesso! alterações feitas no banco de dados. " +
                $"ID PEDIDO: {pedido.Cod_Pedido} | PAGO : {pedido.Pago}");
        }
        #endregion
        #region GetPedidos
        public async Task<MeusPedidosViewModel> GetMeusPedidos(string user_id)
        {

            var meusPedidos = await _context.Pedidos.Where(p => p.Id_User == user_id).ToListAsync();
            if (meusPedidos is not null)
            {
                foreach (Pedido pedido in meusPedidos)
                {
                    // Criar uma nova lista de detalhes do pedido para cada pedido
                    var pedidoDetalhes = await DetalhePedidoList(pedido.Cod_Pedido);
                    pedido._PedidoDetalhes = pedidoDetalhes; // Definir a lista de detalhes do pedido para o pedido atual
                }

                MeusPedidosViewModel meusPedidosViewModel = new MeusPedidosViewModel()
                {
                    Pedidos = meusPedidos
                };
                return meusPedidosViewModel;
            }
            MeusPedidosViewModel meusPedidosViewEmpty = new MeusPedidosViewModel()
            {
                Pedidos = Enumerable.Empty<Pedido>()

            };
            return meusPedidosViewEmpty;

        }
        #endregion
        #region GetProdutos
        public async Task<IEnumerable<Produto>> GetMeusProdutos(int cod_pedido)
        {
            try
            {
                var meuPedido = await _context.Pedidos.FirstOrDefaultAsync(mp => mp.Cod_Pedido == cod_pedido);
                if (meuPedido is not null && !string.IsNullOrEmpty(meuPedido.Conjunto_IdProdutos))
                {
                    string[] Ids = meuPedido.Conjunto_IdProdutos.Split(',');
                    List<Produto> ProdutosEmPedido = new();

                    for (int i = 0; i < Ids.Length; i++)
                    {
                        int idGet = Convert.ToInt32(Ids[i]);
                        var item = await _productService.GetById(idGet);

                        ProdutosEmPedido.Add(item.Value);
                    }
                    await Task.Delay(1000);
                    return ProdutosEmPedido;
                }
                return Enumerable.Empty<Produto>();

            }
            catch (NullReferenceException)
            {
                throw new Exception("Listando os seus pedidos... Por favor, recarregue a página.");
            }

        }

        #endregion
        #region Lista pedido detalhes
        public async Task<List<PedidoDetalhe>> DetalhePedidoList(int id)
        {
            var detalhe = await _context.PedidoDetalhes.Where(p => p.Cod_Pedido == id).ToListAsync();
            if (detalhe is null)
            {
                return new List<PedidoDetalhe>();
            }

            return detalhe;
        }
        #endregion
        #region Get Todos os pedidos
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            var pedidos = await _context.Pedidos.Where(p => p.Pago == true).ToListAsync();
            await Task.Delay(0500);
            return pedidos;
        }
        #endregion
        #region Delete pedido
        public async Task<ActionResult<Pedido>> Delete(int id)
        {
            var pedido = await GetById(id);
            var pedidoDetalhes = await DetalhePedidoList(id);

            if (pedido is not null && pedidoDetalhes is not null)
            {
                foreach (PedidoDetalhe p in pedidoDetalhes)
                {
                    _context.PedidoDetalhes.Remove(p);
                    await _context.SaveChangesAsync();
                }
                _context.Pedidos.Remove(pedido.Value);
                await _context.SaveChangesAsync();
                return new OkObjectResult("O pedido foi excluido com sucesso!");
            }
            return new NotFoundObjectResult("O pedido não foi encontrado!");
        }
        #endregion
        #region Get pedido pelo Id
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
        #endregion
        #region Atualizar pedido
        public async Task<ActionResult<Pedido>> Update(int id, Pedido pedido)
        {
            if (pedido is not null)
            {
                var pedidoExist = await _context.Pedidos.FirstOrDefaultAsync(p => p.Cod_Pedido == id);
                if (pedidoExist is not null)
                {

                    pedidoExist.Nome = pedido.Nome;
                    pedidoExist.DataPedido = pedido.DataPedido;
                    pedidoExist.Logradouro = pedido.Logradouro;
                    pedidoExist.Numero = pedido.Numero;
                    pedidoExist.Telefone = pedido.Telefone;
                    pedidoExist.TotalItensPedido = pedido.TotalItensPedido;
                    pedidoExist.TotalPedido = pedido.TotalPedido;
                    pedidoExist.Entregue = pedido.Entregue;
                    pedidoExist.Concluido = pedido.Concluido;
                    pedidoExist.Pago = true;
                    pedidoExist.Frete = pedido.Frete;

                    _context.Pedidos.Update(pedidoExist);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult($"O pedido ID:{pedido.Cod_Pedido} foi atualizado com sucesso! Data de alteração:{DateTime.Now}");
                }
                return new NotFoundObjectResult("Pedido não encontrado!");
            }
            return new BadRequestObjectResult("O Pedido é nulo!");
        }
        #endregion
        #region Criar pedido
        public async Task<ActionResult> CriarPedido(Pedido pedido, string user_id)
        {
            if (pedido is not null)
            {
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
                pedido.Conjunto_IdProdutos = result;

                pedido.Pago = false;  // importante para definir o pedido temporario..


                await _context.Pedidos.AddAsync(pedido);
                await _context.SaveChangesAsync();


                foreach (var item in carrinhoItems)
                {

                    PedidoDetalhe pedidoDetalhe = new PedidoDetalhe
                    {
                        Cod_Pedido = pedido.Cod_Pedido,
                        Cod_Produto = item.Produto.Cod_Produto,
                        Quantidade = item.QntProduto,
                        Preco = (decimal)(item.Produto.Preco * item.QntProduto),
                        Produto = item.Produto,
                        Conjunto_Pedidos = result,
                        Pedido = pedido,
                        Id_User = user_id,

                    };

                    var produto = await _context.Produtos.FindAsync(item.Produto.Cod_Produto);
                    if (pedido.Agendado)
                    {
                        var quantidade = produto.EstoqueAgendamento - pedidoDetalhe.Quantidade;
                        produto.EstoqueAgendamento = quantidade;
                        _context.Entry(produto).Property(x => x.EstoqueAgendamento).IsModified = true;
                    }
                    else
                    {
                        var quantidade = produto.EmEstoque - pedidoDetalhe.Quantidade;
                        produto.EmEstoque = quantidade;
                        _context.Entry(produto).Property(p => p.EmEstoque).IsModified = true;
                    }
                    _context.PedidoDetalhes.Add(pedidoDetalhe);
                }
                _carrinho.LimparCarrinho();
                await _context.SaveChangesAsync();
                return new OkObjectResult("Pedido criado com sucesso!");
            }
            return new NotFoundObjectResult("O Pedido é nulo!");
        }
        #endregion

        #region Verificar Pedido expirados

        public async Task<ActionResult<Pedido>> VerificarPedidosExpirados()
        {

            var pedidosExpirados = _context.Pedidos.Where(p => p.DataExpiracao <= DateTime.Now).ToList();
            if (pedidosExpirados.Count > 0)
            {
                foreach (var pedido in pedidosExpirados)
                {
                    if (!pedido.Pago)
                    {
                        _context.Pedidos.Remove(pedido);
                        var produtoDetalhe = _context.PedidoDetalhes.Where(p => p.Cod_Pedido == pedido.Cod_Pedido).ToList();
                        if (produtoDetalhe is not null)
                        {
                            foreach (var detail in produtoDetalhe)
                            {
                                _context.PedidoDetalhes.Remove(detail);
                                var produto = await _context.Produtos.FindAsync(detail.Cod_Produto);

                                if (pedido.Agendado)
                                {
                                    var quantidade = produto.EstoqueAgendamento + detail.Quantidade;
                                    produto.EstoqueAgendamento = quantidade;
                                    _context.Entry(produto).Property(x => x.EstoqueAgendamento).IsModified = true;
                                }
                                else
                                {
                                    var quantidade = produto.EmEstoque + detail.Quantidade;
                                    produto.EmEstoque = quantidade;
                                    _context.Entry(produto).Property(x => x.EmEstoque).IsModified = true;
                                }

                            }
                        }

                    }

                }
                var listItemCarrinho = _carrinho.GetItemCarrinhos();
                foreach (var item in listItemCarrinho)
                {
                    if (item is not null)
                    {
                        _context.ItemCarrinhos.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                }
                await _context.SaveChangesAsync();
                return new OkObjectResult($"Produtos acima de 30 min sem pagamento removido do banco de dados: {pedidosExpirados.Count}");
            }
            return new NotFoundObjectResult("Nenhum pedido expirado");

        }
        #endregion
        #region Get Pedido Detalhe pelo ID
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
        #endregion
        #region Produtos do pedido pelo ID pedido
        public List<Produto> ProdutosPedido(int id)
        {
            var produtosPedido = _context.Produtos.Where(p => p.Cod_Produto == id).ToList();
            return produtosPedido;
        }


        #endregion
        #region Verificar proprietario do pedido
        public void VerificarProprietarioDoPedido(string id_user, int codPedidoAnalisar)
        {
            List<Pedido> pedidosDoProprietario = _context.Pedidos.Where(p => p.Id_User == id_user).ToList();
            bool contemAlgumPedidoComEsseId = pedidosDoProprietario.Any(p => p.Cod_Pedido == codPedidoAnalisar);
            if (!contemAlgumPedidoComEsseId)
            {
                throw new DeRosaExceptionValidation("Esse pedido não está em seus pedidos ou foi expirado... Verifique se o link está correto e tente novamente");
            }
        }
        #endregion

    }
}
