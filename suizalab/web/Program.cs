using contracts;
using services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Add(new ServiceDescriptor(typeof(IOrdenesServicio), typeof(OrdenesServicio), ServiceLifetime.Transient));
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
