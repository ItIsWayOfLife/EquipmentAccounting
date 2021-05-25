using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models.Report
{
    public class ReportIndexViewModel
    {
        public SelectList StatusSelect { get; set; }
        public SelectList DepartmentSelect { get; set; }
    }
}
