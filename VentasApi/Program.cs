using Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddTransient<ItiposProductosServices, tiposProductosServices>();
builder.Services.AddTransient<IproductosServices, productosServices>();
builder.Services.AddTransient<IusuariosServices, usuariosServices>();
builder.Services.AddTransient<IpedidosServices, pedidosServices>();
builder.Services.AddTransient<IdetallePedidoServices, detallePedidoServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();