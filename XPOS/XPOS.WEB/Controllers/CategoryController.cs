using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS.WEB.Services;

namespace XPOS.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService category_service;

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
            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {
            TblCategory dataCategory = await category_service.GetById(Id);
            return View(dataCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory data)
        {
            return View();
        }
        public async Task<IActionResult> Detail(int Id)
        {
            TblCategory dataCategory = await category_service.GetById(Id);
            return View(dataCategory);
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
