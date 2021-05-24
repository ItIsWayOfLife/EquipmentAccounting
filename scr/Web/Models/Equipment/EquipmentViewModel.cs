using System.ComponentModel.DataAnnotations;

namespace Web.Models.Equipment
{
    public class EquipmentViewModel
    {
        [Display(Name = "Код")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите инвентарный номер")]
        [Display(Name = "Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [Display(Name = "Код работника")]
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Укажите работника")]
        [Display(Name = "Работник")]
        public string EmployeeFullName { get; set; }

        [Required(ErrorMessage = "Укажите статус")]
        [Display(Name = "Статус")]
        public string StatusEquipmentName { get; set; }

        [Required(ErrorMessage = "Укажите первоначальную стоимость")]
        [Display(Name = "Первоначальная стоимость")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Укажите срок полезного использования")]
        [Display(Name = "Срок полезного использования")]
        public int Term { get; set; }

        [Required(ErrorMessage = "Укажите годовую ставку")]
        [Display(Name = "Годовая ставка")]
        public int ProcentYear { get; set; }

        [Display(Name = "Сумма отчислений в месяц")]
        public string DeductionAmountPerMonth { get; set; }
    }
}
