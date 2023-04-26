using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class APIVariantController : ControllerBase
    {
        private readonly XPos_315Context db; 
        private VMRespons respon = new VMRespons(); //VMRespons adalah model

        public APIVariantController(XPos_315Context _db)//Constructor
        {
            this.db = _db;
        }

        [HttpGet("GetAllVariant")]//untuk memunculkan keseluruhan data sehingga menggunakan List
        public List<VMVariant> GetAllVariant()
        {
            List<VMVariant> dataVariant = new List<VMVariant>();//namavariabelnya adalah datavariant

            dataVariant = (from v in db.TblVariants
                           join c in db.TblCategories
                           on v.IdCategory equals c.Id
                           where v.IsDelete == false
                           select new VMVariant
                           {
                               Id = v.Id,
                               NameVariant = v.NameVariant,
                               Description = v.Description,

                               IdCategory = v.IdCategory,
                               NamaCategory = c.NamaCategory,

                               IsDelete = v.IsDelete


                           }).ToList();


            return dataVariant;//jangan lupa untuk return 
        }

        [HttpGet("GetbyId /{Id}")]//untuk memunculkan atau mendapatkan datu objek sehingga digunakan TblVariant
        public VMVariant GetById(int Id)
        {
            VMVariant dataVariant = new VMVariant();
            dataVariant = (from v in db.TblVariants
                           join c in db.TblCategories 
                           on v.IdCategory equals c.Id
                           where v.Id == Id && v.IsDelete==false
                           select new VMVariant
                           {
                               Id = v.Id,
                               NameVariant = v.NameVariant,
                               Description = v.Description,

                               IsDelete = v.IsDelete,
                               CreateBy = v.CreateBy,
                               CreateDate = v.CreateDate,

                               UpdateBy = v.UpdateBy,
                               UpdateDate = v.UpdateDate,

                               IdCategory = v.IdCategory,
                               NamaCategory = c.NamaCategory
                           }).FirstOrDefault();//yang sebelah kiri dari VMVariant dan yang kanan dari tabel mana

            return dataVariant;
        }

        [HttpPost("PostVariant")]
        public VMRespons PostVariant(TblVariant data)
        {
            data.IsDelete = false;
            data.CreateDate = DateTime.Now;//Ini diisi sesuai dengan tanggal kapan ngisi datanya

            try//fungsi untuk menerima data kalau datanya berhasil dimasukkan
            {
                db.Add(data);
                db.SaveChanges();
                respon.Message = "Data Success Saved";
            }
            catch(Exception e)//fungsi untuk menerima data kalau datanya gagal
            {
                respon.Success = false;
                respon.Message ="failed saved: "  + e.InnerException;
            }
            return respon;
        }
        [HttpPut("PutVariant")]
        public VMRespons PutVariant(TblVariant data)
        {
            TblVariant dataOld = db.TblVariants.Where(b => b.Id == data.Id).FirstOrDefault();

            if (dataOld != null)
            {
                dataOld.NameVariant = data.NameVariant;
                dataOld.Description = data.Description;

                dataOld.UpdateDate = DateTime.Now;
                dataOld.UpdateBy = data.UpdateBy;

                try
                {
                    db.Update(dataOld);
                    db.SaveChanges();
                    respon.Message = "data success updated";
                }
                catch(Exception e)
                {
                    respon.Success = false;
                    respon.Message = "Update failes: " + e.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data not found";
            }
            return respon;
        }

        [HttpDelete("DeleteVariant/{Id}")]
        public VMRespons DeleteVariant(int Id)
        {
            TblVariant data = db.TblVariants.Where(b => b.Id == Id).FirstOrDefault();

            if (data != null)
            {
                data.IsDelete = true;
                try
                {
                    db.Update(data);
                    db.SaveChanges();
                    respon.Message = "Data Success deleted";
                }
                catch(Exception e)
                {
                    respon.Success = false;
                    respon.Message="Delete failed: " + e.Message;
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
