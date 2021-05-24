using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Equipment
{
    public class EquipmentAddEditViewModel
    {
        public EquipmentViewModel EquipmentViewModel { get; set; }
        public SelectList StatusEquipmentsSelect { get; set; }
        public SelectList EmployeeSelect { get; set; }
        public SelectList EquipmentTypeSelect { get; set; }        
    }
}
