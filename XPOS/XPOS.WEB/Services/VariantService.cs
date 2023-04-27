using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.WEB.Services
{
    public class VariantService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();
        

        public VariantService(IConfiguration _config)
        {
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }

        public async Task<List<VMVariant>> AllVariant()
        {
            List<VMVariant> dataVariant = new List<VMVariant>();

            string url = RouteAPI + "api/APIVariant/GetAllVariant";
            string apiRespons = await client.GetStringAsync(url);

            dataVariant = JsonConvert.DeserializeObject<List<VMVariant>>(apiRespons);

            return dataVariant;

        }
        public async Task<TblVariant> GetById(int Id)
        {
            TblVariant data = new TblVariant();

            string url = RouteAPI + $"api/APIVariant/GetbyId /{Id}";
            string apiRespons = await client.GetStringAsync(url);

            data = JsonConvert.DeserializeObject<TblVariant>(apiRespons);

            return data;
        }
        
        public async Task<VMRespons> PostVariant(TblVariant data)
        {
            string Datajson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "api/APIVariant/PostVariant";

            var request = await client.PostAsync(url, content);

            if (request.IsSuccessStatusCode) 
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons .Success = false;
                respons.Message = $"{request.StatusCode}:{request.ReasonPhrase}";

            }
            return respons;
        }

    }
}
