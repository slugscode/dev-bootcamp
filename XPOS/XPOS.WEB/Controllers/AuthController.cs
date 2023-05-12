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


        private int idUser = 1;

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

        [HttpPost]
        public async Task<IActionResult> Register(VMUserCustomer dataRegist)
        {
            VMRespons respon = new VMRespons();

            dataRegist.CreatedBy = idUser;
            dataRegist.IdUser = idUser;

            respon = await auth_service.Register(dataRegist);


            return Json(respon);
        }
        public async Task<JsonResult> CheckEmailDuplicate(string Email)
        {
            bool isDuplicate = await auth_service.CheckEmailDuplicate(Email);

            return Json(isDuplicate);
        }

        public async Task<JsonResult> checklogin(string Email, string Password)
        {

            VMUserCustomer dataUserCustomer = new VMUserCustomer();

            dataUserCustomer = await auth_service.login(Email, Password);

            VMRespons respon = new VMRespons();
            
            if(dataUserCustomer == null)
            {

                respon.Success = false;
                respon.Message = $"Failed Login";


            }
            else
            {
                HttpContext.Session.SetString("NameCustomer", dataUserCustomer.NameCustomer);
                HttpContext.Session.SetInt32("Id", dataUserCustomer.Id);
                HttpContext.Session.SetInt32("IdUser", dataUserCustomer.IdUser);
                HttpContext.Session.SetInt32("IdRole", dataUserCustomer.IdRole);

                respon.Success = true;
                respon.Message = $"Welcome {dataUserCustomer.NameCustomer}";

            }

            return Json(respon);
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();

            return Json(true);
        }

    }
}
