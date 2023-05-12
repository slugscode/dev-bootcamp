using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMMenu
    {
        //Menu
        public int Id { get; set; }
        public string MenuName { get; set; } = null!;
        public string MenuAction { get; set; } = null!;
        public string MenuController { get; set; } = null!;
        public string? MenuIcon { get; set; }
        public int? MenuSorting { get; set; }
        public bool? IsParent { get; set; }
        public int? MenuParent { get; set; }
        public bool? IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? IdRole { get; set; }
        public int? IdMenu { get; set; }
        public List<VMMenu>? listMenuChild { get; set; }
    }
}
