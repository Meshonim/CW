using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class City
    {
        [Display(Name = "City Id")]
        public int CityId { get; set; }
        [Display(Name = "City name")]
        [Required]
        public string CityName { get; set; }
        [Range(0, 7000000000)]
        [Display(Name = "City population")]
        [Required]
        public int CityPopulation { get; set; }

        public virtual ICollection<House> Houses { get; set; }
    }
}
