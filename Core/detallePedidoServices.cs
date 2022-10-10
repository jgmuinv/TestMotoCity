using Models;

namespace Core;

public interface IdetallePedidoServices
{
    public GenericResponse<detallePedido> GetById(Int32 id);
    public GenericResponse<List<detallePedido>> GetAll();
    public GenericResponse<detallePedido> PostAddUpdate(detallePedido obj);
}

public class detallePedidoServices : IdetallePedidoServices
{
    private dbContext _db;

    public detallePedidoServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<detallePedido> GetById(Int32 id)
    {
        var resp = new GenericResponse<detallePedido>();
        try
        {
            resp.data = (from dpd in _db.detallePedidos
                where dpd.id == id
                select dpd).FirstOrDefault();
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
    
    public GenericResponse<List<detallePedido>> GetAll()
    {
        var resp = new GenericResponse<List<detallePedido>>();
        try
        {
            resp.data = (from dpd in _db.detallePedidos
                select dpd).ToList();
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
    
    public GenericResponse<detallePedido> PostAddUpdate(detallePedido obj)
    {
        var resp = new GenericResponse<detallePedido>();
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