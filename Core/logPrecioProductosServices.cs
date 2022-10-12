using Models;

namespace Core;

public interface IlogPrecioProductosServices
{
    public GenericResponse<logPrecioProductos> PostAdd(logPrecioProductos obj);
}

public class logPrecioProductosServices : IlogPrecioProductosServices
{
    private dbContext _db;

    public logPrecioProductosServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<logPrecioProductos> PostAdd(logPrecioProductos obj)
    {
        var resp = new GenericResponse<logPrecioProductos>();
        try
        {
            if (obj.logPrecioProductosID == 0)
            {
                _db.Add(obj);
            }
            else
            {
                resp.data = null;
                resp.message = $"Error: El campo logPrecioProductosID debe ser 0";
                resp.success = false;
                return resp;
            }

            _db.SaveChanges();
            resp.success = true;
            resp.message = "OK";
            resp.data = obj;
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.message = $"Error: {e.Message} {e.InnerException}";
            resp.success = false;
        }
        return resp;
    }
    
}