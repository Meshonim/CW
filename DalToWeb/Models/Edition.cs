using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Edition
    {
        public int EditionId { get; set; }
        public string EditionTitle { get; set; }
        public short EditionYear { get; set; }

        public byte[] EditionImage { get; set; }

        public int HouseId { get; set; }
        public virtual House House { get; set; }

        public short LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public virtual ICollection<Exemplar> Exemplars { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Author> Translators { get; set; }     
    }
}
