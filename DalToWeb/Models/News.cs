using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class News
    {
        [Display(Name = "News Id")]
        public int NewsId { get; set; }
        [Display(Name = "Title")]
        [StringLength(100)]
        [Required]
        public string NewsTitle { get; set; }
        [Display(Name = "Creation date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime NewsCreationDate { get; set; }
        [Display(Name = "Content")]
        [StringLength(500)]
        [Required]
        public string NewsContent { get; set; }
    }
}
