using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Employee
{
    public class EmployeeAddEditViewModel
    {
        public EmployeeViewModel EmployeeViewModel { get; set; }
        public SelectList DepartmentSelect { get; set; }
        public SelectList PositionSelect { get; set; }
    }
}
