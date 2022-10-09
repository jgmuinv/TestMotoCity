using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;

public class productosController : Controller
{
    private IproductosServices _productosServices;

    public productosController(IproductosServices productosServices)
    {
        _productosServices = productosServices;
    }
    
    // GET
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
    
    public IActionResult GetAll()
    {
        var resp = new GenericResponse<List<productos>>();
        try
        {
            resp = _productosServices.GetAll();
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
    
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
}