using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
    public class Cotizaciones
    {
        [Key]
        public int CotizacionId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Clientes? Clientes { get; set; }

        public string Observacion { get; set; }

        public decimal Monto { get; set; }

        public ICollection<CotizacionesDetalle> cotizacionesDetalle { get; set; } = new List<CotizacionesDetalle>();
    }
}
