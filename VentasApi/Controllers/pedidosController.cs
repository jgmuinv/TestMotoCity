using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;

public class pedidosController : Controller
{
    private IpedidosServices _pedidosServices;
    // GET
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