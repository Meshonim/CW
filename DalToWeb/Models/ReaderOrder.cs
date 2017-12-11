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
        [Display(Name = "Order Id")]
        public int ReaderOrderId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of issue")]
        public DateTime ReaderOrderDateOfIssue { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Expiry date")]
        [Required]
        public DateTime ReaderOrderExpiryDate { get; set; }

        [Display(Name = "User Id")]
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [Display(Name = "Exemplar Id")]
        public int ExemplarId { get; set; }
        public virtual Exemplar Exemplar { get; set; }
    }
}
