using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplicadaBlazorParcial2.Models
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public float Monto { get; set; }
        public float Balance { get; set; }
        public bool CobroNoPerdiente { get; set; }
        public double Cobrado { get; set; }
        public virtual Clientes Clientes { get; set; }

        [ForeignKey("VentaId")]
        public virtual List<CobrosDetalles> cobrosDetalles { get; set; } = new List<CobrosDetalles>();
    }
}
