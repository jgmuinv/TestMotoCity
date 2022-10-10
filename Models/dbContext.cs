using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Models;

public class dbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public dbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlCn"));
        }
    }
    
    public DbSet<usuarios> usuarios { get; set; }
    public DbSet<tiposProducto> tiposProducto { get; set; }
    public DbSet<productos> productos { get; set; }
    public DbSet<pedidos> pedidos { get; set; }
    public DbSet<detallePedido> detallePedidos { get; set; }
}