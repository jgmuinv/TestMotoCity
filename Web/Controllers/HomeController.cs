using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var resp = new GenericResponse<List<productos>>();
        var obj = new List<productos>();
        var httpClientHandler = new HttpClientHandler();
        var cliente  = new HttpClient(httpClientHandler);
        cliente.BaseAddress = new Uri("https://localhost:7136/");
        HttpClient client = cliente;
        HttpResponseMessage response = await client.GetAsync("productos/GetAll");
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            Newtonsoft.Json.Linq.JObject tmpJson = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
            resp = JsonConvert.DeserializeObject<GenericResponse<List<productos>>>(tmpJson.ToString());
            obj = resp.data;
        }
        
        return View(obj);
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