using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class VariantController : Controller
    {
        private VariantService variant_service;
        private CategoryService category_service;
        private int IdUser = 1;


        public VariantController(VariantService _variantservice,CategoryService _categoryservice)
        {
            this.variant_service = _variantservice;
            this.category_service = _categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            List<VMVariant> dataVariant = await variant_service.AllVariant();
            return View(dataVariant);
        }

        public async Task<IActionResult> Create()
        {
            VMVariant data = new VMVariant();

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMVariant data)
        {
            data.CreateBy = IdUser;

            VMRespons respon = await variant_service.PostVariant(data);

            if (respon.Success)
            {
                //return RedirectToAction("Index");

                return Json(new {dataRespon = respon });

            }

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            //return View(data);
            return Json(new { dataRespon = respon });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            VMVariant dataVariant = await variant_service.GetById(Id);
            
            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            return PartialView(dataVariant);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(VMVariant data)
        {
            data.UpdateBy = IdUser;

            VMRespons respon = await variant_service.PutVariant(data);
            
            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespons = respon });

            }

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            //return View(data);

            return Json(new { dataRespons = respon });
        }

        public async Task<IActionResult> Detail(int Id)
        {
            VMVariant dataVariant = await variant_service.GetById(Id);
            return PartialView(dataVariant);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            VMVariant data = await variant_service.GetById(Id);
            return PartialView(data);
        }

        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respons = await variant_service.DeleteVariant(Id);

            if (respons.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespons = respons });
            }
            //return RedirectToAction("Delete",Id);
            return Json(new { dataRespons = respons });

        }




    }
}


