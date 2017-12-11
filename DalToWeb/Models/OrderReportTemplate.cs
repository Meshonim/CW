using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class OrderReportTemplate
    {
        [Display(Name = "Order report template Id")]
        public int OrderReportTemplateId { get; set; }
        [Display(Name = "Template name")]
        [StringLength(255)]
        [Required]
        public string TemplateName { get; set; }
        [Display(Name = "Print Background")]
        public bool PrintBackground { get; set; }
        [Display(Name = "Print Day Of Week")]
        public bool PrintDayOfWeek { get; set; }
        [Display(Name = "Print Edition Title")]
        public bool PrintEditionTitle { get; set; }
        [Display(Name = "Maximum count of orders")]
        [Range(1, 1000000)]
        public int MaxCount { get; set; }
    }
}
