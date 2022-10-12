using System.ComponentModel.DataAnnotations;

namespace Models;

public class compras
{
    [Key]
    public Int32 comprasID { get; set; }
    public DateTime fecha { get; set; }
    public Int32 usuariosID { get; set; }
    public Int32 productosID { get; set; }
    public Int32 cantidad { get; set; }
    public decimal total { get; set; }
}