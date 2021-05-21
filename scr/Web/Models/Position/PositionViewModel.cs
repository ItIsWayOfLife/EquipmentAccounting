using System.ComponentModel.DataAnnotations;

namespace Web.Models.Position
{
    public class PositionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите должность")]
        [Display(Name = "Должность")]
        public string Name { get; set; }
    }
}
