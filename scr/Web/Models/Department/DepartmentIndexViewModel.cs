using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.Department
{
    public class DepartmentIndexViewModel
    {
        public List<DepartmentViewModel> DepartmentViewModels { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
