using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Language
    {
        public short LanguageId { get; set; }
        public string LanguageName { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
