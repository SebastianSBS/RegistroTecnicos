using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; } 
        [Required(ErrorMessage = "El campo Nombres es obligatorio..")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Es necesario especificar un sueldo x hora..")]
        [Range (100, 5000, ErrorMessage = "El sueldo debe de estar entre 100 y 5,000...") ]
        public int SueldoHora { get; set; }
    }
}
