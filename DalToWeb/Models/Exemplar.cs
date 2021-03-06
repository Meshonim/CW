﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Exemplar
    {
        [Display(Name = "Exemplar Id")]
        public int ExemplarId { get; set; }
        [Range(1, 1000000)]
        [Display(Name = "Cost")]
        [Required]
        public decimal ExemplarCost { get; set; }

        [Required]
        [Display(Name = "Edition title")]
        public int EditionId { get; set; }
        public virtual Edition Edition { get; set; }

        [Required]
        [Display(Name = "Allocation value")]
        public int AllocationId { get; set; }
        public virtual Allocation Allocation { get; set; }
    }
}
