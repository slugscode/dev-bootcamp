using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class APIProductController : ControllerBase
    {
        private readonly XPos_315Context db;
        private VMRespons respon = new VMRespons();

        public APIProductController(XPos_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllProduct")]
        public List<VMProduct> GetAllProduct()
        {
            List<VMProduct> dataProduct = new List<VMProduct>();

            dataProduct = (from p in db.TblProducts
                           join v in db.TblVariants
                           on p.IdVariant equals v.Id
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where p.IsDelete == false
                           select new VMProduct
                           {
                               Id = p.Id,
                               NameProduct = p.NameProduct,
                               Price = p.Price,
                               Stock = p.Stock,
                               Image = p.Image,

                               NameVariant = v.NameVariant,
                               NamaCategory = c.NamaCategory,
                               
                               IdVariant = p.IdVariant,                                                                                
                               IsDelete = p.IsDelete

                           }).ToList();
            return dataProduct;
    }

        [HttpGet("GetById/{Id}")]
        public VMProduct GetById(int Id)
        {
            VMProduct dataProduct = new VMProduct();

            dataProduct = (from p in db.TblProducts
                           join v in db.TblVariants
                           on p.IdVariant equals v.Id
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where p.Id == Id && p.IsDelete == false
                           select new VMProduct
                           {
                               Id = p.Id,
                               NameProduct = p.NameProduct,

                               Price = p.Price,
                               Stock = p.Stock,
                               Image = p.Image,

                               NameVariant = v.NameVariant,

                               IdCategory= c.Id,
                               IdVariant = v.Id,                        


                           }).FirstOrDefault();

            return dataProduct;
        }
        [HttpPost("PostProduct")]
        public VMRespons PostProduct(TblProduct data)
        {
            data.IsDelete = false;
            data.CreateDate = DateTime.Now;

            try
            {
                db.Add(data);
                db.SaveChanges();
                respon.Message= "Data Success saved";

            }
            catch(Exception e)
            {
                respon.Success = false;
                respon.Message = "Failed Save: " + e.InnerException;

            }
            return respon;
        }

        [HttpPut("PutProduct")]
        public VMRespons PutProduct(TblProduct data)
        {
            TblProduct dataOld = db.TblProducts.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dataOld != null)
            {
                dataOld.NameProduct = data.NameProduct;

                dataOld.Price = data.Price;
                dataOld.Stock = data.Stock;

                //tambahan image 
                if (data.Image != null)
                {
                    data.Image = data.Image;
                }

                dataOld.UpdateDate = DateTime.Now;
                dataOld.UpdateBy =1;

                try
                {
                    db.Update(dataOld);
                    db.SaveChanges();

                    respon.Message = "Data Success Updated";
                }
                catch(Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Update Failed: " + e.Message;

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }
            return respon;
        }

        [HttpDelete("DeleteProduct/{Id}")]
        public VMRespons DeleteProduct(int Id)
        {
            TblProduct data = db.TblProducts.Where(a => a.Id == Id).FirstOrDefault();

            if (data != null)
            {
                data.IsDelete = true;
                try
                {
                    db.Update(data);
                    db.SaveChanges();
                    respon.Message = "Data Success Deleted";
                }
                catch(Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Delete failed:" + e.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }
            return respon;
        }

    }
}
