using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class pedidosController : Controller
{
    private IpedidosServices _pedidosServices;
    
    [HttpGet("{id}")]
    public IActionResult GetById(Int32 id)
    {
        var resp = new GenericResponse<pedidos>();
        try
        {
            resp = _pedidosServices.GetById(id);
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
        var resp = new GenericResponse<List<pedidos>>();
        try
        {
            resp = _pedidosServices.GetAll();
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
    public IActionResult PostAddUpdate(pedidos obj)
    {
        var resp = new GenericResponse<pedidos>();
        try
        {
            resp = _pedidosServices.PostAddUpdate(obj);
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