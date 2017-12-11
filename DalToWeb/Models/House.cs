using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class House
    {
        [Display(Name = "House Id")]
        public int HouseId { get; set; }
        [Display(Name = "House name")]
        [Required]
        public string HouseName { get; set; }
        public string Phone { get; set; }

        [Display(Name = "City Id")]
        [Required]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
