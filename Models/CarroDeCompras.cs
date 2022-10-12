using System.ComponentModel.DataAnnotations;

namespace Models;

public class CarroDeCompras
{
    public Int32 ProductosID { get; set; }
    public string nombre { get; set; }
    public decimal Precio { get; set; }
    public Int32 Cantidad { get; set; }
    
}