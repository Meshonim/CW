using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Allocation
    {
        [Display(Name = "Allocation Id")]
        public int AllocationId { get; set; }

        [Display(Name = "Allocation value")]
        [Required]
        [StringLength(255)]
        public string AllocationValue { get; set; }

        [Display(Name = "Allocation date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime AllocationDate { get; set; }

        public virtual ICollection<Exemplar> Exemplars { get; set; }
    }
}
