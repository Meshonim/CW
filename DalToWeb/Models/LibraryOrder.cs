using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class LibraryOrder
    {
        [Display(Name = "Library order id")]
        public int LibraryOrderId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public bool LibraryOrderStatus { get; set; }

        [Display(Name = "Count")]
        [Range(1, 100)]
        [Required]
        public int LibraryOrderCount { get; set; }

        [Required]
        [Display(Name = "Edition title")]
        public int EditionId { get; set; }
        public virtual Edition Edition { get; set; }
    }
}
