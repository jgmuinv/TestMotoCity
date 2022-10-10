using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class tiposProductoController : Controller
{
    private ItiposProductosServices _tiposProductosServices;

    public tiposProductoController(ItiposProductosServices tiposProductosServices)
    {
        _tiposProductosServices = tiposProductosServices;
    }
    
    [HttpGet("{id}")]
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
    
    [HttpGet]
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
    
    [HttpPost]
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