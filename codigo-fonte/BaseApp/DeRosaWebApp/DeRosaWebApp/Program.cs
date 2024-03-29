using DeRosaWebApp.Context;
using DeRosaWebApp.Models;
using DeRosaWebApp.Repository.Interfaces;
using DeRosaWebApp.Repository.Services;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;
using Stripe;
using ProductService = DeRosaWebApp.Repository.Services.ProductService;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped(sp => Carrinho.GetCarrinho(sp));
builder.Services.AddScoped<ISeedRoleInitial, SeedUserRoleInitial>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var stripeSettings = builder.Configuration.GetSection("Stripe");

// Configura o Stripe com a chave secreta
Stripe.StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",          //adicionando a role Admin como principal
        politica =>
        {
            politica.RequireRole("Admin");
        });
});

builder.Services.AddPaging(options =>                       //adicionando o serviço de paginação
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageIndex";
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()      //introduzindo a classe de contexto do banco de dados
    .AddEntityFrameworkStores<AppDbContext>()                    
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>    //opções para serviço de cadastramento de senha,
{                                                         //podendo escolher os padrões de senha desejados
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(20.1);
    options.Cookie.IsEssential = true;
});

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
CriarPerfisUsuarios(app);

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "acessdenied",
        pattern: "{controller=Account}/{action=AccessDenied}");
});

AutoVerificaPedidosExpirados(app);

app.Run();
void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedRoleInitial>();
        service.SeedRoles();
        service.SeedUsers();
    }
}
void AutoVerificaPedidosExpirados(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    Task.Run(async () =>
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromMinutes(5));

            using (var scope = scopedFactory.CreateScope())
            {
                var pedidoService = scope.ServiceProvider.GetService<IPedidoService>();
                await pedidoService.VerificarPedidosExpirados();
            }
        }
    });
}
