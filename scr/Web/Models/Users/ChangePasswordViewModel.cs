using System.ComponentModel.DataAnnotations;

namespace Web.Models.Users
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }
    }
}
