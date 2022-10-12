using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace Web.Models;

public class CrearProductoDto : productos
{
    public IEnumerable<SelectListItem>? TiposProductos { get; set; }
}