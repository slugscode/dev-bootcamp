using Newtonsoft.Json;
using XPOS_ViewModels;

namespace XPOS.WEB.Services
{
    public class MenuService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;

        private string RouteAPI = "";
        

        public MenuService(IConfiguration _config)
        {
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }

        public async Task<List<VMMenu>> MenuParent(int IdRole)
        {
            List<VMMenu> dataMenu = new List<VMMenu>();

            string url = RouteAPI + $"api/Menu/MenuParent/{IdRole}/";
            string apiRespons = await client.GetStringAsync(url);
            dataMenu = JsonConvert.DeserializeObject<List<VMMenu>>(apiRespons);

            return dataMenu;
        }
    }

}
