using System;
using System.ComponentModel.DataAnnotations;

namespace CW.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "User's e-mail")]
        public string Email { get; set; }

        [Display(Name = "Date of user's registration")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "User's role in the system")]
        public string Role { get; set; }
    }
}