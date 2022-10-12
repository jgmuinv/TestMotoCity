using Models;

namespace Core;

public interface IcomprasServices
{
    public GenericResponse<compras> Save(compras obj);
}

public class comprasServices : IcomprasServices
{
    private dbContext _db;

    public comprasServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<compras> Save(compras obj)
    {
        var resp = new GenericResponse<compras>();
        try
        {
            if (obj.comprasID != 0)
            {
                resp.data = null;
                resp.message = $"Error: El campo comprasID debe ser 0";
                resp.success = false;
                return resp;
            }

            _db.compras.Add(obj);
            _db.SaveChanges();
            
            resp.data = obj;
            resp.message = "OK";
            resp.success = true;
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