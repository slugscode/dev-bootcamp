using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOS_ViewModels
{
    public class VMUserCustomer
    {
        //User
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }

        //Customer
        public int IdUser { get; set; }
        public string NameCustomer { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        //Role
     
        public string? RoleName { get; set; }
        public bool? IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
