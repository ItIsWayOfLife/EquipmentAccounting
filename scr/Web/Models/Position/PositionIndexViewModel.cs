using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.Position
{
    public class PositionIndexViewModel
    {
        public List<PositionViewModel> PositionViewModels { get; set; }
        public SelectList SearchSelection { get; set; }
        public string SearchString { get; set; }
        public string SearchSelectionString { get; set; }
    }
}
