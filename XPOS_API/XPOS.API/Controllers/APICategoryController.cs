using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APICategoryController : ControllerBase
    {
        //Inisialisasi database untuk di controller ini aja 
        private readonly XPos_315Context db;
        private VMRespons respon = new VMRespons();

        public APICategoryController(XPos_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllCategory")] //ini untuk mengeluarkan seluruh data kategori gunakan "getallcategory"
        public List<TblCategory> GetAllCategory()
        {
            List<TblCategory> dataCategory = new List<TblCategory>();
            dataCategory = db.TblCategories.Where(a => a.IsDelete == false).ToList();
            return dataCategory;
        }
        [HttpGet("GetById/{Id}")]//ini untuk mendapatkan suatu objek saja dalam satu row/baris gunakan "getby"
        public TblCategory GetById(int Id)
        {
            TblCategory dataCategory = new TblCategory();
            dataCategory = db.TblCategories.Where(a => a.Id == Id).FirstOrDefault();
            return dataCategory;
        }

        [HttpPost("PostCategory")]//ini untuk user menambahkan data gunakan "post"
        public VMRespons PostCategory(TblCategory data)
        {
            data.IsDelete = false;
            data.CreateDate = DateTime.Now;

            try
            {
                db.Add(data);
                db.SaveChanges();

                respon.Message = "Data success saved";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "failed saved: " + e.InnerException;
            }

            return respon;
        }
        [HttpPut("PutCategory")]//ini untuk mengupdate suatu data gunakan "put"
        public VMRespons PutCategory(TblCategory data)
        {
            //data lama akan diupdate denfan data baru
            TblCategory dataOld = db.TblCategories.Where(a => a.Id == data.Id).FirstOrDefault();
            //jika data ada
            if (dataOld != null)
            {
                //data lama akan diupdate dengan data baru
                dataOld.NamaCategory = data.NamaCategory;
                dataOld.Description = data.Description;

                dataOld.UpdateDate = DateTime.Now;
                dataOld.UpdateBy = data.UpdateBy;

                try//kalau update sukses maka akan masuk ke try
                {
                    db.Update(dataOld);
                    db.SaveChanges();

                    respon.Message = "Data success updated";
                }
                catch (Exception e)//kalau update gagal maka masuk ke catch
                {
                    respon.Success = false;
                    respon.Message = "Update failed : " + e.Message;

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }

            return respon;
        }

        [HttpDelete("DeleteCategory/{Id}")]
        public VMRespons DeleteCategory(int Id)
        {
            TblCategory data = db.TblCategories.Where(a => a.Id == Id).FirstOrDefault();

            if (data != null)
            {
                data.IsDelete = true;

                try
                {
                    db.Update(data);
                    db.SaveChanges();

                    respon.Message = "Data success deleted";
                }
                catch (Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Delete failed :" + e.Message;
 ;               }
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
