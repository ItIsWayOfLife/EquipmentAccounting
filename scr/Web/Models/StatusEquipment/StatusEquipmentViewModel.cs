using System.ComponentModel.DataAnnotations;

namespace Web.Models.StatusEquipment
{
    public class StatusEquipmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите состояние оборудования")]
        [Display(Name = "Состояние оборудования")]
        public string Name { get; set; }
    }
}
