using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.Equipment
{
    public class EquipmentIndexViewModel
    {
        public List<EquipmentViewModel> EquipmentViewModels { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
        public SelectList StatusEquipmentSelect { get; set; }
        public string SearchStatusEquipment { get; set; }
        public SelectList EmployeeSelect { get; set; }
        public string SearchEmployee { get; set; }
        public SelectList EquipmentTypeSelect { get; set; }
        public string SearchEquipmentType { get; set; }
        
    }
}
