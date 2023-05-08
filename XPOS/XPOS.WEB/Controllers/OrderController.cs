using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XPOS.WEB.Services;
using XPOS_ViewModels;

namespace XPOS.WEB.Controllers
{
    public class OrderController : Controller
    {
        private ProductService product_service;
        private OrderService order_service;
        private int IdUser = 1;

        public OrderController(ProductService _productservice, OrderService _orderservice)

        {
            this.product_service = _productservice;
            this.order_service = _orderservice;
        }

        public async Task<IActionResult> Menu()
        {
            List<VMProduct> listOrder = await product_service.AllProduct();
            return View(listOrder);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(VMOrderHeader dataHeader)
        {
            
            return PartialView(dataHeader);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPayment(VMOrderHeader dataHeader)
        {
            VMRespons respon = new VMRespons();

            dataHeader.CreateBy = IdUser;
            dataHeader.IdCustomer = IdUser;

            respon = await order_service.SubmitPayment(dataHeader);


            return Json(respon);
        }

    }
}
