using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClientesId { get; set; }
    [Required(ErrorMessage = "El campo Nombres es obligatorio..")]
    public string Nombres { get; set; }
    [Required(ErrorMessage = "El campo wasa es obligatorio..")]
    public int WhatsApp { get; set; }
}
