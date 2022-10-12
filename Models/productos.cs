using System.ComponentModel.DataAnnotations;

namespace Models;

public class productos
{
    [Required]
    [Display(Name = "Código")]
    [Key]
    public Int32 productosID { get; set; }
    [Required]
    [Display(Name = "Nombre")]
    public string nombre { get; set; }
    [Required]
    [Display(Name = "Precio")]
    public decimal precio { get; set; }
    [Required]
    [Display(Name = "Existencias")]
    public Int32 existencias { get; set; }
    [Required]
    [Display(Name = "Activo")]
    public bool activo { get; set; }
}