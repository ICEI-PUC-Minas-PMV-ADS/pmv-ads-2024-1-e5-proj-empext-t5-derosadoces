using DeRosaWebApp.BusinessRules.Interfaces;
using DeRosaWebApp.BusinessRules.Validations;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeRosaWebApp.Controllers
{
    [Authorize]

    public class PedidoController : Controller
    {
        #region Construtor, propriedades e injeção de dependência
        private readonly Carrinho _carrinho;
        private readonly IPedidoService _pedidoService;
        private readonly IProductService _productService;
        private readonly IPedidoRules _pedidoRules;
        private readonly ICategoriaService _categoriaService;
        private readonly UserManager<IdentityUser> _userMananger;
        public PedidoController(Carrinho carrinho, IPedidoService pedidoService, UserManager<IdentityUser> userManager, IProductService productService, IPedidoRules pedidoRules, ICategoriaService categoriaService)
        {
            _carrinho = carrinho;
            _pedidoService = pedidoService;
            _userMananger = userManager;
            _productService = productService;
            _pedidoRules = pedidoRules;
            _categoriaService = categoriaService;
        }
        #endregion
        #region Meus pedidos
        [HttpGet]
        public async Task<IActionResult> MeusPedidos(string user_id)
        {

            MeusPedidosViewModel pedidos = await _pedidoService.GetMeusPedidos(user_id);
            await Task.Delay(1000);
            return View(pedidos);

        }
        #endregion
        #region Checkout
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var items = _carrinho.GetItemCarrinhos();
            PedidoCheckoutViewModel pedido = new();
            foreach (var item in items)
            {
                var categoria = await _categoriaService.GetById(item.Produto.IdCategoria);
                if (categoria is not null && categoria.CategoriaNome.Equals("Comemorativos"))
                {
                    pedido.PedidosComemorativos.Add(item.Produto);
                }
            }

            return View(pedido);
        }
        #endregion

        #region Checkout completo
        [HttpPost]
        public async Task<IActionResult> Checkout(PedidoCheckoutViewModel pedido)
        {
            try
            {
                // Inicializando variáveis.  
                int totalItemsPedido = 0;                                         
                double precoTotalPedido = 0.0;
                // Pegando todos os itens do carrinho.
                List<ItemCarrinho> itemCarrinhos = _carrinho.GetItemCarrinhos();

                //Atribuindo á  lista de itens do carrinho á propriedade do carrinho.
                _carrinho.ListItemCarrinho = itemCarrinhos;

                // Iterando sobre os itens do carrinho para calcular totalItemsPedido e precoTotalPedido
                foreach (var item in itemCarrinhos)                                                
                {
                    totalItemsPedido += item.QntProduto;
                    precoTotalPedido += item.QntProduto * item.Produto.Preco;
                    // A propriedade pedido.ProdutosPedidos é inicialmente nula, então é adicionado os produtos do carrinho a propriedade.

                    pedido._Pedido.ProdutosPedido.Add(item);

                    // Pegando a categoria de cada produto. 
                    var categoria = await _categoriaService.GetById(item.Produto.IdCategoria);      
                    if (categoria is not null && categoria.CategoriaNome.Equals("Comemorativos"))
                    {
                        // Se ele for comemorativo, é adicionado em uma lista de produtos comemorativos.
                        pedido.PedidosComemorativos.Add(item.Produto);                             
                    }

                }
                // Se o carrinho for nulo, então retorna um erro ao modelo.
                if (_carrinho.ListItemCarrinho.Count == 0)       
                {
                    ModelState.AddModelError("", "Resumo");
                }
                // Se todas as informações forem validas, continua o checkout.
                if (ModelState.IsValid)                       
                {
                    // Pegando o user_id para manuseio do pedido.
                    var user_id = _userMananger.GetUserId(User);

                    //*{ Atribuindo todas as variaveis que foram somadas no codigo acima.
                    pedido._Pedido.TotalItensPedido = totalItemsPedido;                              
                    pedido._Pedido.TotalPedido = precoTotalPedido;
                    pedido._Pedido.DataExpiracao = pedido._Pedido.DataPedido.AddMinutes(30);        
                    pedido._Pedido.Id_User = user_id;
                    //*} O cliente não pode fornecer essas informações, por isso são iniciadas aqui.

                    // Aqui é feito uma verificação para regra de negocio,a verificação causa uma Exceção e o código para aqui caso a regra não seja valida, retornando um aviso ao usuário.
                    _pedidoRules.VerificaComemorativosSeteDiasAntecedencia(pedido.PedidosComemorativos, pedido._Pedido.DataParaEntregar);

                    // Caso o código não tenha a exceção ( ou seja, continue ), então ele cria o pedido.
                    var result = await _pedidoService.CriarPedido(pedido._Pedido, user_id);

                    // O result retorna ''OkObjectResult'' caso tenha sido sucedido, então ele retorna o resumo do pedido para o usuário
                    if (result is not null)     
                    {
                        ViewBag.CheckoutCompletoMensagem = "Resumo do pedido";
                        ViewBag.TotalPedido = precoTotalPedido;

                        return View("Resumo", pedido._Pedido);

                    }
                    // Se for nulo, retorna erro
                    else
                    {
                        ModelState.AddModelError("Erro", "Erro ao realizar pedido!");
                        return View(pedido);
                    }
                }
                // Se o usuário errou alguma informação no checkout, retorna um aviso.
                else
                {
                    ModelState.AddModelError("ErroDados", "Verifique todos os campos e tente novamente!");
                    return View(pedido);
                }
            }
            // Aqui que a regra de negocio funciona, a exceção é capturada e o código é pausado quando ela ocorre.
            catch (PedidoExceptionValidation ex)     
            {
                ModelState.AddModelError("ErroDados", ex.Message);
                return View(pedido);
            }

        }
        #endregion
        #region Resumo
        public async Task<ActionResult<Pedido>> Resumo(Pedido pedido, int cod_pedido)
        {
            try
            {
                var id_user = _userMananger.GetUserId(User);
                _pedidoService.VerificarProprietarioDoPedido(id_user, cod_pedido);   
                await _pedidoService.VerificarPedidosExpirados();

                if (pedido._PedidoDetalhes is null || pedido is null)
                {
                    var getPedido = await _pedidoService.GetById(cod_pedido);
                    pedido.Id_User = getPedido.Value.Id_User;
                    pedido.Nome = getPedido.Value.Nome;
                    pedido.DataPedido = getPedido.Value.DataPedido;
                    pedido.DataExpiracao = getPedido.Value.DataExpiracao;
                    pedido.Cep = getPedido.Value.Cep;
                    pedido.Rua = getPedido.Value.Rua;
                    pedido.Numero = getPedido.Value.Numero;
                    pedido.Complemento = getPedido.Value.Complemento;
                    pedido.Bairro = getPedido.Value.Bairro;
                    pedido.Cidade = getPedido.Value.Cidade;
                    pedido.Estado = getPedido.Value.Estado;
                    pedido.Telefone = getPedido.Value.Telefone;
                    pedido.ProdutosPedido = getPedido.Value.ProdutosPedido;
                    pedido.TotalItensPedido = getPedido.Value.TotalItensPedido;
                    pedido.TotalPedido = getPedido.Value.TotalPedido;
                    pedido.Concluido = getPedido.Value.Concluido;
                    pedido.Entregue = getPedido.Value.Entregue;
                    pedido.Pago = getPedido.Value.Pago;
                    pedido.Conjunto_IdProdutos = getPedido.Value.Conjunto_IdProdutos;

                    var pedidoDetalhe = await _pedidoService.DetalhePedidoList(cod_pedido);


                    foreach (var item in pedidoDetalhe)
                    {
                        var produto = await _productService.GetById(item.Cod_Produto);
                        item.Produto = produto.Value;
                    }
                    pedido._PedidoDetalhes = pedidoDetalhe;

                    ViewBag.CheckoutCompletoMensagem = "Resumo do pedido";
                    ViewBag.TotalPedido = pedido.TotalPedido;
                    await Task.Delay(1000);

                    return View(pedido);
                }
                return View(pedido);
            }
            catch (NullReferenceException)
            {
                ViewBag.Erro = "O pedido foi expirado.";
                return View("Erro");
            }
            catch (PedidoExceptionValidation e)
            {
                ViewBag.Erro = e.Message;
                return View("Erro");
            }
        }
        #endregion
        #region Remover pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> RemovePedido(int cod_pedido)
        {
            var pedido = await _pedidoService.GetById(cod_pedido);
            if (pedido != null)
            {
                await _pedidoService.Remove(pedido.Value);
                return RedirectToAction("Index", "Home");
            }
            return new BadRequestObjectResult("Erro ao remover pedido");
        }
        #endregion
        #region Erro
        public IActionResult Erro(string message)
        {
            if (message is not null)
            {
                ViewBag.Erro = message;
            }
            return View();
        }
        #endregion

    }
}
