using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.StatusEquipment
{
    public class StatusEquipmentIndexViewModel
    {
        public List<StatusEquipmentViewModel> StatusEquipmentViewModels { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
