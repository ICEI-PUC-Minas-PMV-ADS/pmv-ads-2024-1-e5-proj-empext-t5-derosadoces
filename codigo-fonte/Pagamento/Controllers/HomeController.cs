using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pagamento.Models;
using Stripe;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Pagamento.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Payment()
    {
        ViewBag.StripePublishableKey = _configuration["Stripe:PublicKey"];
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(string stripeToken, int paymentAmount)
    {
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
                return RedirectToAction("Success");
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
