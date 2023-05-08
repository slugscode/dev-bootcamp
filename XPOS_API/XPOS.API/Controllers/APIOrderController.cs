using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIOrderController : ControllerBase
    {
        private readonly XPos_315Context db;
        private VMRespons respon = new VMRespons();

        public APIOrderController(XPos_315Context db)
        {
            this.db = db;
        }

        [HttpPost("SubmitPayment")]

        public VMRespons SubmitPayment(VMOrderHeader dataHeader)
        {
            TblOrderHeader head = new TblOrderHeader();

            head.TotalQty = dataHeader.TotalQty;
            head.Amount = dataHeader.Amount;
            head.IdCustomer = dataHeader.IdCustomer;
            head.IsCheckout = true;
            head.CodeTransaction = GenerateCode();

            head.CreateBy = head.CreateBy;
            head.CreateDate = DateTime.Now;
            head.IsDelete = false;

            try
            {
                db.Add(head);
                db.SaveChanges();

                foreach (VMOrderDetail item in dataHeader.ListDetail)
                {
                    TblOrderDetail detail = new TblOrderDetail();

                    detail.IdHeader = head.Id;
                    detail.IdProduct = item.IdProduct;
                    detail.Qty = item.Qty;
                    detail.SubTotal = item.SubTotal;
                    detail.IsDelete = false;
                    detail.CreateBy = dataHeader.CreateBy;
                    detail.CreateDate = DateTime.Now;

                    try
                    {
                        db.Add(detail);
                        db.SaveChanges();

                        TblProduct prod = db.TblProducts.Where(a => a.Id == item.IdProduct).FirstOrDefault();

                        if (prod != null)
                        {
                            prod.Stock = prod.Stock - item.Qty;

                            try
                            {
                                db.Update(prod);
                                db.SaveChanges();

                                respon.Message = "Thanks for order";
                            }
                            catch (Exception e)
                            {
                                respon.Success = false;
                                respon.Message = e.Message;


                            }
                        }
                    }

                    catch (Exception e)
                    {
                        respon.Success = false;
                        respon.Message = e.Message;
                    }

                }
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = e.Message;
            }



            return respon;
        }

        [HttpGet("GenerateCode")]
        public string GenerateCode()
        {
            //XPOS-08052023-00001
            string code = $"XPOS-{DateTime.Now.ToString("ddMMyyyy")}-";
            string digit = "";

            TblOrderHeader dataLast = db.TblOrderHeaders.OrderByDescending(a => a.CodeTransaction).FirstOrDefault();

            if(dataLast == null)
            {
                digit = "00001";
            }
            else
            {
                string defaultDigit = "00000";
                string codeLast = dataLast.CodeTransaction;
                string subCode = codeLast.Substring(15);
                int intCode = int.Parse(subCode);
                intCode++;

                int lenCode = intCode.ToString().Length;
                defaultDigit = defaultDigit + intCode.ToString();
                defaultDigit = defaultDigit.Substring(lenCode, 5);

                digit = defaultDigit;
                

            }
            return code+digit;
        }
    }
}
