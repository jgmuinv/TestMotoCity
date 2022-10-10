using Models;

namespace Core;

public interface IproductosServices
{
    public GenericResponse<productos> GetById(Int32 id);
    public GenericResponse<List<productos>> GetAll();
    public GenericResponse<productos> PostAddUpdate(productos obj);
}

public class productosServices : IproductosServices
{
    private dbContext _db;

    public productosServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<productos> GetById(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        try
        {
            resp.data = (from p in _db.productos
                where p.id == id
                select p).FirstOrDefault();
            resp.success = true;
            resp.message = "OK";
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }

        return resp;
    }

    public GenericResponse<List<productos>> GetAll()
    {
        var resp = new GenericResponse<List<productos>>();
        try
        {
            resp.data = (from p in _db.productos
                select p).ToList();
            resp.message = "OK";
            resp.success = true;
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        return resp;
    }
    
    public GenericResponse<productos> PostAddUpdate(productos obj)
    {
        var resp = new GenericResponse<productos>();
        try
        {
            if (obj.id == 0)
            {
                _db.Add(obj);
            }
            else
            {
                _db.Update(obj);
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