using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
