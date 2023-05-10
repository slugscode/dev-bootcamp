using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAuthController : ControllerBase
    {
        private readonly XPos_315Context db;
        

        public APIAuthController(XPos_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllRole")]

        public List<TblRole> GetAllRole()
        {
            List<TblRole> listRole = db.TblRoles.Where(a => a.IsDelete == false).ToList();

            return listRole;
        }

        [HttpPost("Register/{Id}")]

        public VMUserCustomer Register(VMUserCustomer data)
        {
            VMUserCustomer data = new VMUserCustomer();

            TblUser user = new TblUser();
            //user.

            TblCustomer cutomer = new TblCustomer();
            //
            

            return data;
        }
        
    }
}
