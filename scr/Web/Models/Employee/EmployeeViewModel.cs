using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Employee
{
    public class EmployeeViewModel
{
        [Display(Name = "Код")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите ФИО")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Укажите пол")]
        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Укажите дату рождения")]
        [Display(Name = "Дать рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите должность")]
        [Display(Name = "Должность")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "Укажите отдел")]
        [Display(Name = "Отдел")]
        public string DepartmentName { get; set; }
    }
}
