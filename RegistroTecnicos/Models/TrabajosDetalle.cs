using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class TrabajosDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        [ForeignKey("TrabajoId")]
        public int TrabajoId { get; set; }
        [ForeignKey("ArticuloId")]
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio {  get; set; }
        public decimal Costo { get; set; }
    }
}
