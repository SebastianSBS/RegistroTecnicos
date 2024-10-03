using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;
public class TiposTecnicos
{
    [Key]
    public int TipoTecnicoId { get; set; }
    [ForeignKey("TecnicoId")]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "El tipo de tecnico es requerido")]
    public string Descripcion { get; set; }  
}
