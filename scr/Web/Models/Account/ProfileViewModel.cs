using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ведите фамилию")]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Введите отчетство")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Укажите пол")]
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Укажите дату рождения")]
        [Display(Name = "Дать рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
