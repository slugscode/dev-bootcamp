using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using XPOS_ViewModels;

namespace XPOS.WEB.Services
{
    public class OrderService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respon = new VMRespons();

        public OrderService(IConfiguration _config)
        {

            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }

        public async Task<VMRespons> SubmitPayment(VMOrderHeader dataHeader)
        {
            string Datajson = JsonConvert.SerializeObject(dataHeader);

            var content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + $"api/APIOrder/SubmitPayment";

            var request = await client.PostAsync(url,content);

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

        public async Task<List<VMOrderHeader>> OrderHistory()
        {
            List<VMOrderHeader> data = new List<VMOrderHeader>();

            string url = RouteAPI + $"api/APIOrder/OrderHistory/";
            string apiRespons = await client.GetStringAsync(url);
            data = JsonConvert.DeserializeObject<List<VMOrderHeader>>(apiRespons);


            return data;

        }
    }
}
