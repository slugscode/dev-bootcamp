using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;
using XPOS.API.Models;
using XPOS_ViewModels;

namespace XPOS.WEB.Services
{
    public class CategoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();


        public CategoryService(IConfiguration _config)
        {

            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }
        public async Task<List<TblCategory>> AllCategory()
        {
            List<TblCategory> dataCategory = new List<TblCategory>();

            //memanggil alamat endpoint API dan mendapatkan data json
            string url = RouteAPI + $"api/APICategory/GetAllCategory";
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
        public async Task<VMRespons> PutCategory(TblCategory data)
        {
            //proses mengubah objek menjadi string 
            string Datajson = JsonConvert.SerializeObject(data);

            //proses mengubah string menjadi json yang bisa dibaca API
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "api/ApiCategory/PutCategory";

            //proses meanggil Endpoint API dan kirim data body

            var request = await client.PutAsync(url, content);

            //jika berhasil hasil APInya
            if (request.IsSuccessStatusCode)
            {
                //Membaca hasil repson dari API
                var apiRespons = await request.Content.ReadAsStringAsync();

                //convert ke object vmRespons
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);

            }
            else
            {
                respons.Success = false;
                respons.Message=$"{request.StatusCode} : {request.ReasonPhrase}";
            }

            return respons;
        }
        public async Task<VMRespons> PostCategory(TblCategory data)
        {
            string Datajson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "api/ApiCategory/PostCategory";

            var request =await client.PostAsync(url, content);
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
        public async Task<VMRespons> DeleteCategory(int id)
        {
            string url = RouteAPI + $"api/ApiCategory/DeleteCategory/{id}";
            var request = await client.DeleteAsync(url);

            if (request.IsSuccessStatusCode)
            {
                var apiRespon = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespon);
            }
            return respons;
        }
    }
   
}
