using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.Employee
{
    public class EmployeeIndexViewModel
    {
        public List<EmployeeViewModel> EmployeeViewModels { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
        public SelectList DepartmentSelect { get; set; }
        public string SearchDepartment{ get; set; }
        public SelectList PositionSelect { get; set; }
        public string SearchPosition { get; set; }
        public bool Move { get; set; }
        public int? EquipmentId { get; set; }
    }
}
