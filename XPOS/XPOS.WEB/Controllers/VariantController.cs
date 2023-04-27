using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class VariantController : Controller
    {
        private VariantService variant_service;
        private int IdUser = 1;
        public VariantController(VariantService _variant_service)
        {
            this.variant_service = _variant_service;
        }

        public async Task<IActionResult> Index()
        {
            List<VMVariant> dataVariant = await variant_service.AllVariant();

            return View(dataVariant);
        }

        public IActionResult Create()
        {
            TblVariant variant = new TblVariant();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TblVariant data)
        {
            data.CreateBy = IdUser;
            VMRespons respons = await variant_service.PostVariant(data);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }
            return View(data);

        }

    }
}


