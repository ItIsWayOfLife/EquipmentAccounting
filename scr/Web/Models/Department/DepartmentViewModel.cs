using System.ComponentModel.DataAnnotations;

namespace Web.Models.Department
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите отдел")]
        [Display(Name = "Отдел")]
        public string Name { get; set; }
    }
}
