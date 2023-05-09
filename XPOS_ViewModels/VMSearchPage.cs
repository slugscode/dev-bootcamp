using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMSearchPage
    {
        public string sortOrder { get; set; }
        public string searchString { get; set; }
        public string currentFilter { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }

    }
}
