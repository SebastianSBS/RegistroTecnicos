using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public int ClienteId { get; set; }
    [ForeignKey("TecnicoId")]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage = "El campo descripcion es necesario..")]
    public string Descripcion { get; set; }
    [Required(ErrorMessage = "El campo monto es necesario..")]
    public int Monto { get; set; }
}
