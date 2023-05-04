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

        public async Task<IActionResult> Index(VMSearchPage pg)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "name_desc" : "";
            ViewBag.IdSort =pg.sortOrder == "Id" ? "Id_desc" :"Id";

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
            
            List<VMVariant> dataVariant = await variant_service.AllVariant();

            if (!String.IsNullOrEmpty(pg.searchString))
            {
                dataVariant = dataVariant.Where(a => a.NameVariant.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }


            switch (pg.sortOrder)
            {
                case "Id":
                    dataVariant = dataVariant.OrderBy(a => a.Id).ToList();
                    break;
                case "Id_desc":
                    dataVariant = dataVariant.OrderByDescending(a => a.Id).ToList();
                    break;
                case "name_desc":
                    dataVariant = dataVariant.OrderByDescending(a => a.NameVariant).ToList();
                    break;
                default:
                    dataVariant = dataVariant.OrderBy(a => a.NameVariant).ToList();
                    break;
            }

            //return View(dataVariant);
            return View(await PaginatedList<VMVariant>.CreateAsync(dataVariant, pg.pageNumber ?? 1, pg.pageSize ?? 3));
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


