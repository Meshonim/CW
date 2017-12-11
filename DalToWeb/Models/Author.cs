using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Author
    {
        [Display(Name = "Auhor Id")]
        public int AuthorId { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string AuthorFirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string AuthorMiddleName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string AuthorLastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; private set; }

        public virtual ICollection<Edition> Editions { get; set; }
        public virtual ICollection<Edition> TranslatedEditions { get; set; }
        public virtual ICollection<Edition> IllustratedEditions { get; set; }
    }
}
