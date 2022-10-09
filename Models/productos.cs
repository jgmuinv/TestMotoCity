namespace Models;

public class productos
{
    public Int32 id { get; set; }
    public Int32 tipoId { get; set; }
    public string nombre { get; set; }
    public decimal precio { get; set; }
    public Int32 existencias { get; set; }
}