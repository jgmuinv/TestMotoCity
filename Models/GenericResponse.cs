namespace Models;

public class GenericResponse<T>
{
    public bool success { get; set; }
    public string message { get; set; }
    public T? data { get; set; }
}