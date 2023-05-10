using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class AuthController : Controller
    {
        private AuthService auth_service;

        private readonly IWebHostEnvironment webHost;


        private int Iduser = 1;

        public AuthController(AuthService _authservice, IWebHostEnvironment _webHost)
        {
            this.auth_service = _authservice;
            this.webHost = _webHost;

        }


        public IActionResult Login()
        {
            return PartialView();
        }

    
        public async Task<IActionResult> Register()
        {

            VMUserCustomer data = new VMUserCustomer();

            List<TblRole> listRole = await auth_service.GetAllRole();

            ViewBag.ListRole = listRole;

            return PartialView(data);
        }

        //public async Task<JsonResult> Register(VMUserCustomer data)
        //{
        //    data.CreatedBy = Iduser;

        //    VMRespons respon = await auth_service.Register(data);

        //    ViewBag.ListRole = await auth_service.GetAllRole();

        
        //    return Json(respon);
        //}

        //public async Task<JsonResult> CheckEmailDuplicate(string Email)
        //{
        //    bool isDuplicate = await auth_service.CheckEmailDuplicate(Email);
        //    return Json(isDuplicate);
        //}
    }
}
