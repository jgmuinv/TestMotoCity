using Microsoft.AspNetCore.Mvc;
using Models;
using Web.Utils;

namespace Web.Controllers;

public class CarroDeCompraController : Controller
{
    // GET
    public IActionResult Index()
    {
        var obj = new List<CarroDeCompras>();
        try
        {
            
            var items = new List<CarroDeCompras>();
            items.Add(new CarroDeCompras()
            {
                ProductosID = 1,
                Cantidad = 10,
                Precio = 345.50m
            });
            
            
            HttpContext.Session.SetObjectAsJson("carroSession",items);
            
            var MiCarro = HttpContext.Session.GetObjectFromJson<List<CarroDeCompras>>("carroSession");
            if (MiCarro != null)
            {
                obj = MiCarro;

                foreach (var item in obj)
                {
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return View(obj);
    }
}