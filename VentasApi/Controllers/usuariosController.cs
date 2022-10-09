using Core;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace VentasApi.Controllers;

public class usuariosController : Controller
{
    private IusuariosServices _usuariosServices;
    // GET
    public IActionResult GetById(Int32 id)
    {
        var resp = new GenericResponse<usuarios>();
        try
        {
            resp = _usuariosServices.GetById(id);
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
        var resp = new GenericResponse<List<usuarios>>();
        try
        {
            resp = _usuariosServices.GetAll();
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        
        return Ok(resp);
    }
    
    public IActionResult PostAddUpdate(usuarios obj)
    {
        var resp = new GenericResponse<usuarios>();
        try
        {
            resp = _usuariosServices.PostAddUpdate(obj);
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