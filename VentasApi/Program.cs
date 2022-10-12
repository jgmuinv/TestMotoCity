using Core;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddTransient<IproductosServices, productosServices>();
builder.Services.AddTransient<IusuariosServices, usuariosServices>();
builder.Services.AddTransient<IpedidosServices, pedidosServices>();
builder.Services.AddTransient<IdetallePedidoServices, detallePedidoServices>();
builder.Services.AddTransient<IlogPrecioProductosServices, logPrecioProductosServices>();
builder.Services.AddTransient<IcomprasServices, comprasServices>();

builder.Services.AddDbContext<dbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();