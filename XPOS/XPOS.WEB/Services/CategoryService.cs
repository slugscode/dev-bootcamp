using Newtonsoft.Json;
using XPOS.API.Models;

namespace XPOS.WEB.Services
{
    public class CategoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";


        public CategoryService(IConfiguration _config)
        {

            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }
        public async Task<List<TblCategory>> AllCategory()
        {
            List<TblCategory> dataCategory = new List<TblCategory>();

            //memanggil alamat endpoint API dan mendapatkan data json
            string url = RouteAPI + "api/APICategory/GetAllCategory";
            string apiRespons = await client.GetStringAsync(url);//await sebagai fungsi ruang tunggu

            //proses mengubah string json ke list object
            dataCategory = JsonConvert.DeserializeObject<List<TblCategory>>(apiRespons);

            return dataCategory;
        }

        public async Task<TblCategory> GetById(int Id)
        {
            TblCategory data = new TblCategory();

            string url = RouteAPI + $"api/APICategory/GetById/{Id}";
            string apiRespons = await client.GetStringAsync(url);

            data = JsonConvert.DeserializeObject<TblCategory>(apiRespons);

            return data;
        } 
    }
   
}
