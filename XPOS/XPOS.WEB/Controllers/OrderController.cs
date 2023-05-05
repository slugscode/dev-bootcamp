using Microsoft.AspNetCore.Mvc;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class OrderController : Controller
    {
        private ProductService product_service;

        public OrderController(ProductService _productservice)

        {
            this.product_service = _productservice;
        }

        public async Task<IActionResult> Menu()
        {
            List<VMProduct> listOrder = await product_service.AllProduct();
            return View(listOrder);
        }
    }
}
