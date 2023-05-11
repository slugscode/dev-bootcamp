﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAuthController : ControllerBase
    {
        private readonly XPos_315Context db;

        VMRespons respon = new VMRespons();


        public APIAuthController(XPos_315Context _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllRole")]
        public List<TblRole> GetAllRole()
        {
            List<TblRole> listRole = db.TblRoles.Where(a => a.IsDelete == false).ToList();

            return listRole;
        }
        [HttpGet("GetById/{Id}")]
        public TblRole GetById(int Id)

        {
            TblRole dataRole = new TblRole();

            dataRole = db.TblRoles.Where(a => a.Id == Id).FirstOrDefault();

            return dataRole;
        }

        [HttpPost("Register")]
        public VMRespons Register(VMUserCustomer dataRegist)
        {

            TblUser user = new TblUser();

            user.Email = dataRegist.Email;
            user.Password = dataRegist.Password;
            user.CreateBy = dataRegist.CreatedBy;
            user.CreateDate = DateTime.Now;

            user.IdRole = dataRegist.IdRole;
            user.IsDelete = false;

            try
            {
                db.Add(user);
                db.SaveChanges();

                TblCustomer cust = new TblCustomer();

                cust.Id = dataRegist.Id;
                cust.IdUser = dataRegist.IdUser;
                cust.NameCustomer = dataRegist.NameCustomer;
                cust.Phone = dataRegist.Phone;
                cust.Address = dataRegist.Address;
                cust.CreateBy = dataRegist.CreatedBy;
                cust.CreateDate = DateTime.Now;

                try
                {
                    db.Add(cust);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    respon.Success = false;
                    respon.Message = e.Message;
                }


            }

            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = e.Message;
            }

            return respon;
        }

        [HttpGet("CheckEmailDuplicate/{Email}")]
        public bool CheckEmailDuplicate(string Email)
        {
            bool isDuplicate = false;

            TblUser dataEmail = new TblUser();
            dataEmail = db.TblUsers.Where(a => a.Email == Email).FirstOrDefault();

            if (dataEmail != null)
            {
                isDuplicate = true;
            }

            return isDuplicate;
        }
        [HttpGet("Login/{Email}/{Password}")]

        public VMUserCustomer Login(string Email,string Password)
        {
            VMUserCustomer data = new VMUserCustomer();
                
            
            //data = (from user in db.TblUser
            //        join customer in db.TblCustomer
            //   on )
               

            if (data != null)
            {
                isExist = true;
            }

            return isExist;


        }

    }
}
