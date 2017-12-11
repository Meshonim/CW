using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class LibraryReportTemplate
    {
        [Display(Name = "Library report template Id")]
        public int LibraryReportTemplateId { get; set; }
        [Display(Name = "Template name")]
        [StringLength(255)]
        [Required]
        public string TemplateName { get; set; }
        [Display(Name = "Print headers")]
        public bool PrintHeaders { get; set; }
        [Display(Name = "Autosized")]
        public bool Autosized { get; set; }
        [Display(Name = "Maximum count of orders")]
        [Range(1, 1000000)]
        public int MaxCount { get; set; }
    }

}
