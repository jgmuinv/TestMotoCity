using Models;

namespace Core;

public interface ItiposProductosServices
{
    public GenericResponse<tiposProducto> GetById(Int32 id);
    public GenericResponse<List<tiposProducto>> GetAll();
    public GenericResponse<tiposProducto> PostAddUpdate(tiposProducto obj);
}

public class tiposProductosServices : ItiposProductosServices
{
    private dbContext _db;
    
    public tiposProductosServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<tiposProducto> GetById(Int32 id)
    {
        var resp = new GenericResponse<tiposProducto>();
        try
        {
            resp.data = (from tp in _db.tiposProductos
                where tp.id == id
                select tp).First();
            resp.success = true;
            resp.message = "OK";
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.message = $"Error: {e.Message} {e.InnerException}";
            resp.success = false;
        }
        return resp;
    }

    public GenericResponse<tiposProducto> PostAddUpdate(tiposProducto obj)
    {
        var resp = new GenericResponse<tiposProducto>();
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

    public GenericResponse<List<tiposProducto>> GetAll()
    {
        var resp = new GenericResponse<List<tiposProducto>>();
        try
        {
            resp.data = (from tp in _db.tiposProductos
                select tp).ToList();
            resp.success = true;
            resp.message = "OK";
        }
        catch (Exception e)
        {
            resp.success = false;
            resp.message = $"Error: {e.Message} {e.InnerException}";
            resp.data = null;
        }
        return resp;

    }

}