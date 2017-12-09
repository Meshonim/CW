using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class House
    {
        public int HouseId { get; set; }
        public string HouseName { get; set; }
        public string Phone { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
