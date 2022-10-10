using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class detallePedidoController : Controller
{
    private IdetallePedidoServices _detallePedidoServices;
    
    [HttpGet("{id}")]
    public IActionResult GetById(Int32 id)
    {
        var resp = new GenericResponse<detallePedido>();
        try
        {
            resp = _detallePedidoServices.GetById(id);
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var resp = new GenericResponse<List<detallePedido>>();
        try
        {
            resp = _detallePedidoServices.GetAll();
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
    
    [HttpPost]
    public IActionResult PostAddUpdate(detallePedido obj)
    {
        var resp = new GenericResponse<detallePedido>();
        try
        {
            resp = _detallePedidoServices.PostAddUpdate(obj);
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