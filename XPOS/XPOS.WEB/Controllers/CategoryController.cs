using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService category_service;
        private int IdUser = 1;

        public CategoryController(CategoryService _categoryservice)
        {
            this.category_service = _categoryservice;
        }
        public async Task<IActionResult> Index()
        {
            List<TblCategory> dataCategory = await category_service.AllCategory();

            return View(dataCategory);
        }
        public IActionResult Create()
        {
            TblCategory category = new TblCategory();
            return PartialView(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TblCategory data)
        {
            data.CreateBy = IdUser;
            VMRespons respons = await category_service.PostCategory(data);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            TblCategory dataCategory = await category_service.GetById(Id);
            return PartialView(dataCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory data)
        {
            data.UpdateBy = IdUser;
            VMRespons respons = await category_service.PutCategory(data);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }
            return View(data);
           
        }
        public async Task<IActionResult> Detail(int Id)
        {
            TblCategory dataCategory = await category_service.GetById(Id);
            return PartialView(dataCategory);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            TblCategory data = await category_service.GetById(Id);
            return PartialView(data);
        }
        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respon = await category_service.DeleteCategory(Id);

            if (respon.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete",Id);

        }

    }
}
