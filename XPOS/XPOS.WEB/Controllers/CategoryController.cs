using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService category_service;
        private readonly IHttpContextAccessor contextAccessor;
        private int IdUser = 1;

        public CategoryController(CategoryService _categoryservice, IHttpContextAccessor _contextAssessor)
        {
            this.category_service = _categoryservice;
            this.contextAccessor = _contextAssessor;
            this.IdUser = contextAccessor.HttpContext.Session.GetInt32("IdUser") ?? 1;
        }
        public async Task<IActionResult> Index(VMSearchPage pg)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "name_desc" : "";
            ViewBag.IdSort = pg.sortOrder == "Id" ? "Id_desc" : "Id";

            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;

            if (pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.currentFilter;
            }
            ViewBag.CurrentFilter = pg.searchString;

            List<TblCategory> dataCategory = await category_service.AllCategory();

            if (!String.IsNullOrEmpty(pg.searchString))
            {
                dataCategory = dataCategory.Where(a => a.NamaCategory.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }

            switch (pg.sortOrder)
            {
                case "Id":
                    dataCategory = dataCategory.OrderBy(a => a.Id).ToList();
                    break;
                case "Id_desc":
                    dataCategory = dataCategory.OrderByDescending(a => a.Id).ToList();
                    break;
                case "name_desc":
                    dataCategory = dataCategory.OrderByDescending(a => a.NamaCategory).ToList();
                    break;
                default:
                    dataCategory = dataCategory.OrderBy(a => a.NamaCategory).ToList();
                    break;
            }

            return View(await PaginatedList<TblCategory>.CreateAsync(dataCategory,pg.pageNumber?? 1,pg.pageSize ?? 3));
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
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respons });
            }
            //return View(data);
            return Json(new { dataRespon = respons });
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
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respons });
            }
            //return View(data);
            return Json(new { dataRespon = respons });
           
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
                //return RedirectToAction("Index");
                return Json(new {dataRespon = respon });
            }

            //return RedirectToAction("Delete",Id);
            return Json(new { dataRespon = respon });

        }
    }
}
