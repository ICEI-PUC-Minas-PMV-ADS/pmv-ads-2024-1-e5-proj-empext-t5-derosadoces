using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Diagnostics;

namespace DeRosaWebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPedidoService _pedidoService;
        private readonly Carrinho _carrinho;

        public PaymentController(ILogger<PaymentController> logger, IConfiguration configuration, IPedidoService pedidoService, Carrinho carrinho)
        {
            _logger = logger;
            _configuration = configuration;
            _pedidoService = pedidoService;
            _carrinho = carrinho;
        }
        [HttpGet]
        [Route("Payment/Payment")]
        public async Task<IActionResult> Payment(int cod_pedido)
        {
            var pedido = await _pedidoService.GetById(cod_pedido);
            
            ViewBag.StripePublishableKey = _configuration["Stripe:PublicKey"];
            return View(pedido.Value);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(string stripeToken, int paymentAmount, int cod_pedido) 
        {
            var pedido = await _pedidoService.GetById(cod_pedido);

            if (string.IsNullOrEmpty(stripeToken))
            {
                _logger.LogError("Stripe token está vazio.");
                return View("Error");
            }

            if (paymentAmount <= 0)
            {
                _logger.LogError("O valor do pagamento é inválido.");
                return View("Error");
            }

            try
            {
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = paymentAmount, // Agora o valor vem do usuário
                    Currency = "usd", // Ajuste a moeda conforme necessário
                    Description = "Descrição do produto ou serviço",
                    Source = stripeToken,
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);

                if (charge.Paid)
                {
                    pedido.Value.Pago = true;
                    var updatePayed = await _pedidoService.UpdatePayment(pedido.Value.Cod_Pedido, pedido.Value.Pago);
                    _carrinho.LimparCarrinho();
                    return RedirectToAction("Success", updatePayed.Result);
                }
                else
                {
                    _logger.LogError("Falha ao processar o pagamento.");
                    return RedirectToAction("Error");
                }
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Erro Stripe: {Message}", ex.Message);
                return RedirectToAction("Error");
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
