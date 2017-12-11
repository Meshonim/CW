using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Genre
    {
        [Display(Name = "Genre Id")]
        public short GenreId { get; set; }
        [Display(Name = "Genre name")]
        [Required]
        public string GenreName { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
