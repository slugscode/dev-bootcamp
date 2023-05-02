using Microsoft.AspNetCore.Mvc;
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
        private int Iduser = 1;

        //ini namanya constructor
        public ProductController(ProductService _productservice,VariantService _variantservice,CategoryService _categoryservice)
        {
            this.product_service = _productservice;
            this.variant_service = _variantservice;
            this.category_service = _categoryservice;

        }

        public async Task<IActionResult> Index()
        {
            List<VMProduct> dataProduct = await product_service.AllProduct();
            return View(dataProduct);
        }

        public async Task<IActionResult> Create()
        {
            VMProduct data = new VMProduct();
            List<TblCategory> listCategory = await category_service.AllCategory();
            List<VMVariant> listVariant = await variant_service.AllVariant();

            
            ViewBag.ListVariant = listVariant;
            ViewBag.ListCategory = listCategory;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMProduct data)
        {
            data.CreateBy = Iduser;
            VMRespons respons = await product_service.PostProduct(data);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }

            List<VMVariant> listVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = listVariant;

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            return View(data);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            VMProduct dataProduct = await product_service.GetById(Id);
            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;

            return View(dataProduct);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(VMProduct data)
        {
            data.UpdateBy = Iduser;

            VMRespons respons = await product_service.PutProduct(data);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }

            List<TblCategory> listCategory = await category_service.AllCategory();
            ViewBag.ListCategory = listCategory;
            List<VMVariant> listVariant = await variant_service.AllVariant();
            ViewBag.ListVariant = listVariant;

            return View(data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            VMProduct dataProduct = await product_service.GetById(Id);
            return View(dataProduct);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            VMProduct data = await product_service.GetById(Id);
            return View(data);
        }

        public async Task<IActionResult> SureDelete(int Id)
        {
            VMRespons respons = await product_service.DeleteProduct(Id);

            if (respons.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", Id);

        }





    }
}
