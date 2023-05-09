using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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

        
        public async Task<IActionResult> OrderHistory(VMSearchPage pg)
        {
            List<VMOrderHeader> dataOrder = await order_service.OrderHistory();

            ViewBag.CurrentSort = pg.sortOrder;
            ViewBag.pageSize = pg.pageSize;

            ViewBag.FilterMinDate = pg.MinDate;
            ViewBag.FilterMaxDate = pg.MaxDate;

            ViewBag.FilterMinPrice = pg.MinPrice ?? 0;
            ViewBag.FilterMaxPrice = pg.MaxPrice ?? 0;

            var cultureinfo = new System.Globalization.CultureInfo("en-US");

            if (pg.MinDate != null)
            {
                string MinDate = pg.MinDate?.ToString("MM/dd/yyyy");
                pg.MinDate = DateTime.Parse(MinDate, cultureinfo);
            }
            

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
                dataOrder = dataOrder.Where(a => a.CodeTransaction.ToLower().Contains(pg.searchString.ToLower())).ToList();
            }

            if (pg.MinPrice != null || pg.MaxPrice != null)
            {
                pg.MinPrice = pg.MinPrice == null ? decimal.MinValue : pg.MinPrice;
                pg.MaxPrice = pg.MaxPrice == null ? decimal.MaxValue : pg.MaxPrice;

                dataOrder = dataOrder.Where(a => a.Amount >= pg.MinPrice && a.Amount <= pg.MaxPrice).ToList();

            }

            if (pg.MinDate != null || pg.MaxDate != null)
            {
                pg.MinDate = pg.MinDate == null ? DateTime.MinValue: pg.MinDate;
                pg.MaxDate= pg.MaxDate == null ? DateTime.MaxValue : pg.MaxDate;

                dataOrder = dataOrder.Where(a => a.CreateDate >= pg.MinDate && a.CreateDate <= pg.MaxDate).ToList();

            }

            return View(await PaginatedList<VMOrderHeader>.CreateAsync(dataOrder,pg.pageNumber?? 1,pg.pageSize ?? 3));
        }

        #region addons function
        public async Task<IActionResult> Search(VMSearchPage pg)
        {
            return PartialView();
        }

        #endregion

    }
}
