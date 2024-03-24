var builder = WebApplication.CreateBuilder(args);

// Carrega a configuração do Stripe do appsettings.json
var stripeSettings = builder.Configuration.GetSection("Stripe");

// Configura o Stripe com a chave secreta
Stripe.StripeConfiguration.ApiKey = stripeSettings["SecretKey"];

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
