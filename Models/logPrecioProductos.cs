using System.ComponentModel.DataAnnotations;

namespace Models;

public class logPrecioProductos
{
    [Key]
    public Int32 logPrecioProductosID { get; set; }
    public DateTime fecha { get; set; }
    public productos Productos { get; set; }
    public Int32 productosID { get; set; }
    public decimal precioAntes { get; set; }
    public decimal precioDespues { get; set; }
}