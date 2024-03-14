using DeRosaWebApp.Models;
using DeRosaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Components
{
    public class CarrinhoComponent : ViewComponent
    {
        private readonly Carrinho _carrinho;
        public CarrinhoComponent(Carrinho carrinho)
        {
            _carrinho = carrinho;
        }
        public IViewComponentResult Invoke()
        {
            var itens = _carrinho.GetItemCarrinhos();
            _carrinho.ListItemCarrinho = itens;
            var carrinhoVM = new CarrinhoViewModel
            {
                CarrinhoCompra = _carrinho,
                CarrinhoCompraTotal = _carrinho.GetTotalCarrinho()
            };
            return View(carrinhoVM);
        }
    }
}
