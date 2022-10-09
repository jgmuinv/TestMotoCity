namespace Models;

public class detallePedido
{
    public Int32 id { get; set; }
    public Int32 pedidoId { get; set; }
    public Int32 productoId { get; set; }
    public Int32 cantidad { get; set; }
}