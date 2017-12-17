using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW.ViewModels
{
    public class AllocationViewModel
    {
        [Display(Name = "Allocation Id")]
        public int AllocationId { get; set; }

        [Display(Name = "Allocation value")]
        public string AllocationValue { get; set; }

        [Display(Name = "Allocation date")]
        [DataType(DataType.Date)]
        public DateTime AllocationDate { get; set; }

        [Display(Name = "Exemplars count")]
        public int ExemplarsCount { get; set; }
    }
}