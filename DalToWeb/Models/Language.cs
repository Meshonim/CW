using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Language
    {
        [Display(Name = "Language Id")]
        public short LanguageId { get; set; }
        [Display(Name = "Language name")]
        [Required]
        public string LanguageName { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
