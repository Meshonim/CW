using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
