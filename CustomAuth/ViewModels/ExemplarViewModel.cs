using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW.ViewModels
{
    public class ExemplarViewModel
    {
        [Display(Name = "Exemplar Id")]
        public int ExemplarId { get; set; }

        [Display(Name = "Cost")]
        public decimal ExemplarCost { get; set; }

        [Display(Name = "Edition title")]
        public string EditionTitle { get; set; }

        [Display(Name = "Allocation value")]
        public string AllocationValue { get; set; }
    }
}