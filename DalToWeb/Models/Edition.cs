using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Edition
    {
        [Display(Name = "Edition Id")]
        public int EditionId { get; set; }
        [Display(Name = "Title")]
        public string EditionTitle { get; set; }
        [Display(Name = "Year")]
        public short EditionYear { get; set; }

        public byte[] EditionImage { get; set; }
        [Display(Name = "House Id")]
        public int HouseId { get; set; }
        public virtual House House { get; set; }
        [Display(Name = "Language Id")]
        public short LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public virtual ICollection<Exemplar> Exemplars { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Author> Translators { get; set; }     
    }
}
