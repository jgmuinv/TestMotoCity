using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class productosController : Controller
{
    private IproductosServices _productosServices;

    public productosController(IproductosServices productosServices)
    {
        _productosServices = productosServices;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        try
        {
            resp = _productosServices.GetById(id);
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
    
    [HttpGet("{name}/{activos}")]
    public IActionResult GetByName(string name, bool activos)
    {
        var resp = new GenericResponse<List<productos>>();
        try
        {
            resp = _productosServices.GetByName(name, activos);
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
    public IActionResult GetAll(bool activos)
    {
        var resp = new GenericResponse<List<productos>>();
        try
        {
            resp = _productosServices.GetAll(activos);
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
    public IActionResult PostAddUpdate(productos obj)
    {
        var resp = new GenericResponse<productos>();
        try
        {
            resp = _productosServices.PostAddUpdate(obj);
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        
        try
        {
            resp = _productosServices.Delete(id);
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