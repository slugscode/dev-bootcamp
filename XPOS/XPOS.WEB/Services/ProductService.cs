using Newtonsoft.Json;
using System.Text;
using XPOS_ViewModels;

namespace XPOS.WEB.Services
{
    public class ProductService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();


        public ProductService(IConfiguration _config)
        {
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];            
        }

        public async Task<List<VMProduct>> AllProduct()
        {
            List<VMProduct> dataProduct = new List<VMProduct>();

            string url = RouteAPI + "api/APIProduct/GetAllProduct";
            string apiRespons = await client.GetStringAsync(url);

            dataProduct = JsonConvert.DeserializeObject<List<VMProduct>>(apiRespons);
            
            return dataProduct;
        }

        public async Task<VMProduct> GetById(int Id)
        {
            VMProduct data = new VMProduct();

            string url = RouteAPI + $"api/APIProduct/GetById/{Id}";
            string apiRespons = await client.GetStringAsync(url);

            data = JsonConvert.DeserializeObject<VMProduct>(apiRespons);
            return data;

        }

        public async Task<VMRespons> PutProduct(VMProduct data)
        {
            string datajson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(datajson, UnicodeEncoding.UTF8, "application/json");
            string url = RouteAPI + "api/APIProduct/PutProduct";
            var request = await client.PutAsync(url, content);

            if (request.IsSuccessStatusCode)
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons.Success = false;
                respons.Message = $"{request.StatusCode}:{request.ReasonPhrase}";
            }
            return respons;

        }

        public async Task<VMRespons> PostProduct(VMProduct data)
        {
            string Datajson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "api/APIProduct/PostProduct";

            var request = await client.PostAsync(url, content);

            if (request.IsSuccessStatusCode)
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons.Success = false;
                respons.Message = $"{request.StatusCode}:{request.ReasonPhrase}";

            }
            return respons;
        }
        public async Task<VMRespons> DeleteProduct(int Id)
        {

            string url = RouteAPI + $"api/APIProduct/DeleteProduct/{Id}";

            var request = await client.DeleteAsync(url);

            if (request.IsSuccessStatusCode)
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons.Success = false;
                respons.Message = $"{request.StatusCode}:{request.ReasonPhrase}";

            }
            return respons;
        }



    }
}
