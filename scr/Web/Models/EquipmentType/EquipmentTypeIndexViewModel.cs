using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.EquipmentType
{
    public class EquipmentTypeIndexViewModel
    {
        public List<EquipmentTypeViewModel> EquipmentTypeViewModel { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
