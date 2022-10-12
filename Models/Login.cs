using System.ComponentModel.DataAnnotations;

namespace Models;

public class Login
{
    [Required]
    public string Usuario { get; set; }
    [DataType(DataType.Password)]
    [Required]
    public string Clave { get; set; }

    public string Resultado { get; set; }
}