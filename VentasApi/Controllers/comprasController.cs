using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;
[ApiController]
[Route("[controller]/[action]")]

public class comprasController : Controller
{
    private IcomprasServices _comprasServices;

    public comprasController(IcomprasServices comprasServices)
    {
        _comprasServices = comprasServices;
    }
    // GET
    [HttpPost]
    public IActionResult PostAdd(compras obj)
    {
        var resp = new GenericResponse<compras>();
        try
        {
            resp = _comprasServices.Save(obj);
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
}