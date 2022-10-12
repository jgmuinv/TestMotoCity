using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Core;

public interface IproductosServices
{
    public GenericResponse<productos> GetById(Int32 id);
    public GenericResponse<List<productos>> GetAll(bool activos);
    public GenericResponse<productos> PostAddUpdate(productos obj);
    public GenericResponse<List<productos>> GetByName(string name, bool activos);
    public GenericResponse<productos> Delete(Int32 id);
}

public class productosServices : IproductosServices
{
    private dbContext _db;
    private IlogPrecioProductosServices _logPrecioProductosServices;

    public productosServices(dbContext db, IlogPrecioProductosServices logPrecioProductosServices)
    {
        _db = db;
        _logPrecioProductosServices = logPrecioProductosServices;
    }

    public GenericResponse<productos> GetById(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        try
        {
            resp.data = (from p in _db.productos
                where p.productosID == id
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
    
    public GenericResponse<List<productos>> GetByName(string name, bool activos)
    {
        var resp = new GenericResponse<List<productos>>();
        try
        {
            resp.data = (from p in _db.productos
                where (p.nombre.Contains(name)  || name == "-")
                    && p.activo == activos
                      orderby p.nombre
                select p).ToList();
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

    public GenericResponse<List<productos>> GetAll(bool activos)
    {
        var resp = new GenericResponse<List<productos>>();
        try
        {
            resp.data = (from p in _db.productos
                where p.activo == activos
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

    public GenericResponse<productos> Delete(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        try
        {

            var objFind = _db.productos.Find(id);

            if (objFind == null)
            {
                resp.success = false;
                resp.message = $"Error: No existe producto con id {id}";
                resp.data = null;
                return resp;
            }

            if (objFind.activo==false)
            {
                resp.success = false;
                resp.message = $"Error: No se puede eliminar un registro ya eliminado";
                resp.data = null;
                return resp;
            }

            objFind.activo = false;
            
            _db.Update(objFind);

            _db.SaveChanges();
            resp.success = true;
            resp.message = "OK";
            resp.data = objFind;
        }
        catch (Exception e)
        {
            resp.data = null;
            resp.message = $"Error: {e.Message} {e.InnerException}";
            resp.success = false;
        }
        return resp;
    }
    
    public GenericResponse<productos> PostAddUpdate(productos obj)
    {
        var resp = new GenericResponse<productos>();
        try
        {
            if (obj.productosID == 0)
            {
                obj.activo = true;
                _db.Add(obj);
            }
            else
            {
                var objOld = GetById(obj.productosID);
                if (objOld.success && objOld.data != null)
                {
                    if (objOld.data.precio != obj.precio)
                    {
                        var objLog = new logPrecioProductos()
                        {
                            fecha = DateTime.Now,
                            productosID = obj.productosID,
                            logPrecioProductosID = 0,
                            precioAntes = objOld.data.precio,
                            precioDespues = obj.precio
                        };
                        _db.logPrecioProductos.Add(objLog);
                        _db.SaveChanges();
                    }
                    _db.ChangeTracker.Clear();
                    _db.productos.Update(obj);
                    _db.SaveChanges();

                    resp.success = true;
                    resp.message = "OK";
                    resp.data = obj;
                }
            }
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