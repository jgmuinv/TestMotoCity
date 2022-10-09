using Models;

namespace Core;

public interface IusuariosServices
{
    public GenericResponse<usuarios> GetById(Int32 id);
    public GenericResponse<List<usuarios>> GetAll();
    public GenericResponse<usuarios> PostAddUpdate(usuarios obj);
}

public class usuariosServices : IusuariosServices
{
    private dbContext _db;

    public usuariosServices(dbContext db)
    {
        _db = db;
    }

    public GenericResponse<usuarios> GetById(Int32 id)
    {
        var resp = new GenericResponse<usuarios>();
        try
        {
            resp.data = (from u in _db.usuarios
                where u.id == id
                select u).First();
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

    public GenericResponse<List<usuarios>> GetAll()
    {
        var resp = new GenericResponse<List<usuarios>>();
        try
        {

        }
        catch (Exception e)
        {
            resp.success = false;
            resp.data = null;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        return resp;
    }

    public GenericResponse<usuarios> PostAddUpdate(usuarios obj)
    {
        var resp = new GenericResponse<usuarios>();
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
            resp.data = null;
            resp.message = $"Error: {e.Message} {e.InnerException}";
        }
        return resp;
    }
}