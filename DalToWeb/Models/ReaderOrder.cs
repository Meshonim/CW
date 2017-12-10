using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class ReaderOrder
    {
        public int ReaderOrderId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReaderOrderDateOfIssue { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReaderOrderExpiryDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ExemplarId { get; set; }
        public virtual Exemplar Exemplar { get; set; }
    }
}
