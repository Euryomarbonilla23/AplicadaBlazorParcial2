using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AplicadaBlazorParcial2.Models
{
    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public int ConteoCobro { get; set; }
        public double TotalesCobro { get; set; }
        public float Cobro { get; set; }
        public string Observacion { get; set; }

        [ForeignKey("CobroId")]
        public virtual List<CobrosDetalles> cobrosDetalles { get; set; } = new List<CobrosDetalles>();
        public Cobros()
        {
            this.Fecha = DateTime.Now;
        }
    }
}
