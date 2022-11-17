using Restaurante_Delicias.ADO;

var builder = WebApplication.CreateBuilder(args);

//Registrar el servico para Inyeccion de Dependencias
builder.Services.AddSingleton<IProductoADO, ProductoADO>();
builder.Services.AddSingleton<ILoginADO, LoginADO>();
builder.Services.AddSingleton<IDet_boletaADO, Det_boletaADO>();
builder.Services.AddSingleton<ICab_boletaADO, Cab_boletaADO>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agregar servicio para la sesion
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    //Eliminar
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

//Indicamos que debe usar sesion
app.UseSession();

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
    pattern: "{controller=Login}/{action=index}/{id?}");

app.Run();
