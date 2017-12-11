using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Rule
    {
        [Display(Name = "Rule Id")]
        public int RuleId { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Rule value")]
        public string RuleValue { get; set; }

        [Required]
        [Range(1, 3)]
        [Display(Name = "Rule priority")]
        public byte RulePriority { get; set; }
    }
}
