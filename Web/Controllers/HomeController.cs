using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Models;
using Newtonsoft.Json;
using Web.Models;
using Web.Utils;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Borrar(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        var obj = new productos();
        
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        
        HttpResponseMessage response = await client.GetAsync($"productos/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
            obj = resp.data;
        }
        return View(obj);
    }

    [HttpPost]
    public async Task<IActionResult> Borrar(productos producto)
    {
        var resp = new GenericResponse<productos>();
        var obj = new productos();
        
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        
        HttpResponseMessage response = await client.DeleteAsync($"productos/Delete/{producto.productosID}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
            obj = resp.data;
        }

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Index(productosAComprarDto producto)
    {
        var resp = new GenericResponse<List<productosAComprarDto>>();
        var obj = new List<productosAComprarDto>();
        
        var nombre = string.IsNullOrEmpty(producto.nombre) ? "-" : producto.nombre;
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        // client.DefaultRequestHeaders.Accept.Clear();
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //string? data = null;
        HttpResponseMessage response = await client.GetAsync($"productos/GetByName/{nombre}/{true}");
        if (response.IsSuccessStatusCode)
        {
            //data = await response.Content.ReadAsStringAsync();
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<List<productosAComprarDto>>>(tmpJson.ToString());
            obj = resp.data;
        }

        
        return View(obj);
    }

    [HttpGet]
    public async Task<IActionResult> Comprar(Int32 id)
    {
        var resp = new GenericResponse<productosAComprarDto>();
        var obj = new productosAComprarDto();
        var respProducto = new GenericResponse<productos>();
        var objProducto = new productos();
        try
        {
            var httpClientHandler = new HttpClientHandler();
            var cliente  = new HttpClient(httpClientHandler);
            cliente.BaseAddress = new Uri("https://localhost:7136/");
            HttpClient client = cliente;
        
            HttpResponseMessage response = await client.GetAsync($"productos/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
                respProducto = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
                objProducto = respProducto.data;
            }

            if (objProducto == null)
            {
                return RedirectToAction("Index");
            }

            objProducto.existencias -= 1;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage UpdateExistencias = await client.PostAsJsonAsync($"productos/PostAddUpdate",objProducto);
            if (UpdateExistencias.IsSuccessStatusCode)
            {
                var result = await UpdateExistencias.Content.ReadAsStringAsync();
                Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
                respProducto = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
                objProducto = respProducto.data;
            }
            
            var compra = new compras()
            {
                productosID = objProducto.productosID,
                usuariosID = HttpContext.Session.GetObjectFromJson<usuarios>("user").id,
                cantidad = 1,
                fecha = DateTime.Now,
                comprasID = 0,
                total = objProducto.precio
            };
            
            var respCompras = new GenericResponse<compras>();
            var objCompras = new compras();
            // var httpClientHandler = new HttpClientHandler();
            // var cliente  = new HttpClient(httpClientHandler);
            // cliente.BaseAddress = new Uri("https://localhost:7136/");
            // HttpClient client = cliente;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseCompras = await client.PostAsJsonAsync($"compras/PostAdd",compra);
            if (responseCompras.IsSuccessStatusCode)
            {
                var result = await responseCompras.Content.ReadAsStringAsync();
                Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
                respCompras = JsonConvert.DeserializeObject<GenericResponse<compras>>(tmpJson.ToString());
                objCompras = respCompras.data;
            }

            obj.cantidadAComprar = 1;
            obj.nombre = objProducto.nombre;
            obj.precio = objProducto.precio;



        }
        catch (Exception e)
        {
            resp.data = null;
            resp.message = $"{e.Message} {e.InnerException}";
            resp.success = false;
        }

        return View(obj);
    }
    
    [HttpGet]
    public IActionResult Salir()
    {
        HttpContext.Session.Remove("user");
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Entrar()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Entrar(Login login)
    {
        var resp = new GenericResponse<Login>();
        var obj = new Login();
        
        var respUsuarios = new GenericResponse<usuarios>();
        var objUsuarios = new usuarios();
        
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        
        HttpResponseMessage response = await client.GetAsync($"usuarios/GetLogin/{login.Usuario}/{login.Clave}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            respUsuarios = JsonConvert.DeserializeObject<GenericResponse<usuarios>>(tmpJson.ToString());
            objUsuarios = respUsuarios.data;
        }

        if (objUsuarios != null)
        {
            login.Resultado = "OK";
            HttpContext.Session.SetObjectAsJson("user", objUsuarios);
            return RedirectToAction("Index");
        }
        else
        {
            login.Resultado = "Credenciales no validas";
            return View(login);
        }
        
        
    }

    public IActionResult  CrearProducto()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> EditarProducto(Int32 id)
    {
        var resp = new GenericResponse<productos>();
        var obj = new productos();
        
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        
        HttpResponseMessage response = await client.GetAsync($"productos/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
            obj = resp.data;
        }
        return View(obj);
    }

    [HttpPost]
    public async Task<IActionResult> EditarProducto(productos producto)
    {
        // HttpContext.Session.Set("AnyKey", "Hello World!");
        if (!ModelState.IsValid)
        {
            return View(producto);
        }
        var resp = new GenericResponse<productos>();
        var obj = new productos();
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsJsonAsync($"productos/PostAddUpdate",producto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
            obj = resp.data;
        }
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult>  CrearProducto(productos producto)
    {
        var resp = new GenericResponse<productos>();
        var obj = new productos();
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
         client.DefaultRequestHeaders.Accept.Clear();
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         HttpResponseMessage response = await client.PostAsJsonAsync($"productos/PostAddUpdate",producto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<productos>>(tmpJson.ToString());
            obj = resp.data;
        }
        
        return RedirectToAction("Index");
    }
    
    private async Task<IEnumerable<SelectListItem>> ObtenerTiposProductos()
    {
        var resp = new GenericResponse<List<tiposProducto>>();
        var obj = new List<tiposProducto>();
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
	
        HttpResponseMessage response = await client.GetAsync("tiposProducto/GetAll");
	
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<List<tiposProducto>>>(tmpJson.ToString());
            obj = resp.data;
        }
            
        return obj.Select(x => new SelectListItem(x.nombre, x.tiposProductoID.ToString()));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}