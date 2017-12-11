using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class EditionType
    {
        [Display(Name = "Edition Type Id")]
        public int EditionTypeId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(50)]
        public string EditionTypeName { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(255)]
        public string EditionTypeDescription { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
