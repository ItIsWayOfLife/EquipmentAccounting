using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Employee;

namespace Web.Models.Equipment
{
    public class EquipmentAddEditViewModel
    {
        public EquipmentViewModel EquipmentViewModel { get; set; }
        public SelectList StatusEquipmentsSelect { get; set; }
        public SelectList EmployeeSelect { get; set; }
    }
}
