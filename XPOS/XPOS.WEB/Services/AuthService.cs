using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.WEB.Services
{
    public class AuthService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respon = new VMRespons();

        public AuthService(IConfiguration _config)
        {
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }

        public async Task<List<TblRole>> GetAllRole()
        {
            
            List<TblRole> listRole = new List<TblRole>();

            string url = RouteAPI + "api/APIAuth/GetAllRole";
            string apiRespons = await client.GetStringAsync(url);

            listRole = JsonConvert.DeserializeObject<List<TblRole>>(apiRespons);

            return listRole;
        }

        public async Task<VMRespons> Register(VMUserCustomer dataRegist)
        {
            string Datajson = JsonConvert.SerializeObject(dataRegist);
            var content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");
            string url = RouteAPI + $"api/APIAuth/Register";

            var request = await client.PostAsync(url, content);

            if (request.IsSuccessStatusCode)
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respon = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode}:{request.ReasonPhrase}";
            }

            return respon;
        }
        public async Task<bool> CheckEmailDuplicate (string Email)
        {
            bool isDuplicate = false;

            TblUser dataEmail = new TblUser();

            string apiRespons = await client.GetStringAsync(RouteAPI + $"api/APIAuth/CheckEmailDuplicate/{Email}");

            isDuplicate = JsonConvert.DeserializeObject<bool>(apiRespons);

            return isDuplicate;
            
        }

        public async Task<VMUserCustomer> login(string Email,string Password)
        {
            VMUserCustomer dataUserCustomer = new VMUserCustomer();            

            string apiRespons = await client.GetStringAsync(RouteAPI + $"api/APIAuth/Login/{Email}/{Password}");
            dataUserCustomer = JsonConvert.DeserializeObject<VMUserCustomer>(apiRespons);

            return dataUserCustomer;
        }

    }
}
