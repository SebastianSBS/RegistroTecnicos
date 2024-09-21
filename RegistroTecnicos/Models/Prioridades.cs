using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Prioridades
    {
        [Key]
        public int PrioridadId { get; set; }
        [Required(ErrorMessage = "El campo descripcion es obligatorio...")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo tiempo es obligatorio..."  )]
        public int Tiempo { get; set; }
    }
}
