using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;

public class tiposProductoController : Controller
{
    private ItiposProductosServices _tiposProductosServices;
    // GET
    public IActionResult GetById(Int32 id)
    {
        var resp = new GenericResponse<tiposProducto>();
        try
        {
            resp = _tiposProductosServices.GetById(id);
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
        var resp = new GenericResponse<List<tiposProducto>>();
        try
        {
            resp = _tiposProductosServices.GetAll();
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
    
    public IActionResult PostAddUpdate(tiposProducto obj)
    {
        var resp = new GenericResponse<tiposProducto>();
        try
        {
            resp = _tiposProductosServices.PostAddUpdate(obj);
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