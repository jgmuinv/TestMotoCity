using System.ComponentModel.DataAnnotations;

namespace Models;

public class productos
{
    [Required]
    [Display(Name = "Código")]
    public Int32 productosID { get; set; }
    public tiposProducto? tiposProducto { get; set; }
    [Required]
    [Display(Name = "Tipo")]
    public Int32 tiposProductoID { get; set; }
    [Required]
    [Display(Name = "Nombre")]
    public string nombre { get; set; }
    [Required]
    [Display(Name = "Precio")]
    public decimal precio { get; set; }
    [Required]
    [Display(Name = "Existencias")]
    public Int32 existencias { get; set; }
}