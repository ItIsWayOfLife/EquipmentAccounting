using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FLP { get; set; }

        [Required(ErrorMessage = "Email not specified")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
