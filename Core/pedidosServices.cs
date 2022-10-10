using Models;

namespace Core;

public interface IpedidosServices
{
    public GenericResponse<pedidos> GetById(Int32 id);
    public GenericResponse<List<pedidos>> GetAll();
    public GenericResponse<pedidos> PostAddUpdate(pedidos obj);
}

public class pedidosServices : IpedidosServices
{
    private dbContext _db;

    public pedidosServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<pedidos> GetById(Int32 id)
    {
        var resp = new GenericResponse<pedidos>();
        try
        {
            resp.data = (from pd in _db.pedidos
                    where pd.id == id
                        select pd).FirstOrDefault();
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

    public GenericResponse<List<pedidos>> GetAll()
    {
        var resp = new GenericResponse<List<pedidos>>();
        try
        {
            resp.data = (from pd in _db.pedidos
                select pd).ToList();
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

    public GenericResponse<pedidos> PostAddUpdate(pedidos obj)
    {
        var resp = new GenericResponse<pedidos>();
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
            resp.data = obj;
            resp.message = "OK";
            resp.success = true;
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