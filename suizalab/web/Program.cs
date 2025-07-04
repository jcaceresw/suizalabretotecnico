var builder = WebApplication.CreateBuilder(args);
//builder.Services.Add(new ServiceDescriptor(typeof()))
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
