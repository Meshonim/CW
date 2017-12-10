using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Exemplar
    {
        public int ExemplarId { get; set; }
        [Range(1, 1000000)]
        public decimal ExemplarCost { get; set; }

        public int EditionId { get; set; }
        public virtual Edition Edition { get; set; }
    }
}
