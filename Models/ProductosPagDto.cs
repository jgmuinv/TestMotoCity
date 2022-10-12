using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models;

public class ProductosPagDto
{
    public Int32 currentPageIndex { get; set; }
    public Int32 pageCount { get; set; }
    [Display(Name = "Nombre")]
    public string nombre { get; set; }
    public List<productos> productos { get; set; }
    [Required]
    [Display(Name = "Tipo")]
    public Int32 tiposProductoID { get; set; }
    [Display(Name = "Tipos")]
    public IEnumerable<SelectListItem>? tiposProductos { get; set; }
}