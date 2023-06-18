using FinalProject.Persistence.ResponseModels;
using RestSharp;
using System.Web;

namespace FinalProject.Core.Helpers
{
    public static class RequestHelper
    {
        private const string API_KEY = "sk-oiUt648ed920d51cd1302";


        private static RestClient GetClient()
        {
            var options = new RestClientOptions("https://perenual.com/api");
            return new RestClient(options);
        }

        public static async Task<string> GetPlantImage(string latinPlantname)
        {
            var urlEscapedName = HttpUtility.UrlEncode(latinPlantname);
            var request = new RestRequest($"/species-list?q={urlEscapedName}&key={API_KEY}");
            var response = await GetClient().GetAsync<SearchPlantResponse>(request);

            if (!response.data.Any())
            {
                return string.Empty;
            }

            var data = response.data.First();

            if (data.default_image == null)
            {
                // NOTE: We found the image, but this is outside the free api, so just show a plant icon.
                return "https://cdn-icons-png.flaticon.com/512/45/45777.png";
            }

            return data.default_image.small_url;
        }

    }
}
