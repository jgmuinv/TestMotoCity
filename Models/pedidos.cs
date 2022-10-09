namespace Models;

public class pedidos
{
    public Int32 id { get; set; }
    public DateTime fecha { get; set; }
    public Int32 usuarioId { get; set; }
    public decimal total { get; set; }
}