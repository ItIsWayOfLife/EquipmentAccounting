using System.ComponentModel.DataAnnotations;

namespace Web.Models.EquipmentType
{
    public class EquipmentTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите вид оборудования")]
        [Display(Name = "Вид оборудования")]
        public string Name { get; set; }
    }
}
