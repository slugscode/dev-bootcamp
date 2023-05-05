using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using XPOS.API.Models;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class ProductController : Controller
    {
        private VariantService variant_service;
        private CategoryService category_service;
        private ProductService product_service;
        private readonly IWebHostEnvironment webHost;

        private int Iduser = 1;

        //ini namanya constructor
        public ProductController(ProductService _productservice,
            VariantService _variantservice,
            CategoryService _categoryservice,
            IWebHostEnvironment _webHost)

        {
            this.product_service = _productservice;
            this.variant_service = _variantservice;
            this.category_service = _categoryservice;
            this.webHost = _webHost;

        }
        #region CRUD
        
        public async Task<IActionResult> Index(VMSearchPage pg)
        {
            List<VMProduct> dataProduct = await product_service.AllProduct();

            ViewBag.NameSort = String.IsNullOrEmpty(pg.sortOrder) ? "name_desc" : "";
            ViewBag.IdSort = pg.sortOrder == "Id" ? "Id_desc" : "Id";

            ViewBag.PriceSort = pg.sortOrder == "Price" ? "Price_desc" : "Price";

            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;

            ViewBag.FilterMinPrice = pg.MinPrice ?? 0;
            ViewBag.FilterMaxPrice = pg.MaxPrice ?? 0;


            if (pg.searchString != null)
            {
                pg.pageNumber = 1;
            }
            else
            {
                pg.searchString = pg.currentFilter;
            }

            ViewBag.CurrentFilter = pg.searchString;

            if (!String.IsNullOrEmpty(pg.searchString))
            {
                dataProduct = dataProduct.Where(a => a.NameProduct.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }

            if (pg.MinPrice !=null || pg.MaxPrice !=null)
            {
                pg.MinPrice = pg.MinPrice == null ? decimal.MinValue : pg.MinPrice;
                pg.MaxPrice = pg.MaxPrice == null ? decimal.MaxValue : pg.MaxPrice;

                dataProduct = dataProduct.Where(a => a.Price >= pg.MinPrice && a.Price <= pg.MaxPrice).ToList();
            }

            switch (pg.sortOrder)
            {
              
                case "Price":
                    dataProduct = dataProduct.OrderBy(a => a.Price).ToList();
                    break;
                case "Price_desc":
                    dataProduct = dataProduct.OrderByDescending(a => a.Price).ToList();
                    break;
                case "Id":
                    dataProduct = dataProduct.OrderBy(a => a.Id).ToList();
                    break;
                case "Id_desc":
                    dataProduct = dataProduct.OrderByDescending(a => a.Id).ToList();
                    break;
                case "name_desc":
                    dataProduct = dataProduct.OrderByDescending(a => a.NameProduct).ToList();
                    break;
                default:
                    dataProduct = dataProduct.OrderBy(a => a.NameProduct).ToList();
                    break;
            }

            //int pageSize = 3;
            return View(await PaginatedList<VMProduct>.CreateAsync(dataProduct, pg.pageNumber ?? 1, pg.pageSize ?? 3));
        }

        public async Task<IActionResult> Create()
        {
            VMProduct data = new VMProduct();

            List<TblCategory> listCategory = await category_service.AllCategory();

            List<VMVariant> listVariant = await variant_service.AllVariant();

            
            ViewBag.ListVariant = listVariant;
            ViewBag.ListCategory = listCategory;

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMProduct data)
        {
            data.CreateBy = Iduser;
            data.Image = Upload(data);

            VMRespons respon = await product_service.PostProduct(data);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });

            }

            List<VMVariant> listVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = listVariant;

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            //return View(data);

            return Json(new { dataRespon = respon });
        }

        public async Task<IActionResult> Edit(int Id)
        {
            VMProduct dataProduct = await product_service.GetById(Id);

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            List<VMVariant> listVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = listVariant;

            return PartialView(dataProduct);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(VMProduct data)
        {
            data.UpdateBy = Iduser;
            data.Image = Upload(data);

            VMRespons respon = await product_service.PutProduct(data);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            List<VMVariant> listVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = listVariant;

            //return View(data);

            return Json(new { dataRespon = respon });
        }

        public async Task<IActionResult> Detail(int Id)
        {
            VMProduct dataProduct = await product_service.GetById(Id);
            return PartialView(dataProduct);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            VMProduct data = await product_service.GetById(Id);

            return PartialView(data);
        }

        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respon = await product_service.DeleteProduct(Id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            //return RedirectToAction("Delete", Id);
            return Json(new { dataRespon = respon });

        }

        #endregion

        #region addons function

        public async Task<IActionResult> Search()
        {            
            return PartialView();
        }

        public string Upload(VMProduct data)
        {
            string fileName = "";

            if (data.ImageFile != null)
            {
                string uploadfolder = Path.Combine(webHost.WebRootPath, "images");

                fileName = data.ImageFile.FileName;

                string filePath = Path.Combine(uploadfolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    data.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public async Task<JsonResult> GetVariantByCategory(int idcategory)
        {
            List<VMVariant> listVariant = await variant_service.GetVariantByCategory(idcategory);

            return Json(listVariant);
        }
             
        #endregion






    }
}
