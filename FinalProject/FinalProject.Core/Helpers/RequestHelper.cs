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
            const string DEFAULT_IMAGE = "https://cdn-icons-png.flaticon.com/512/45/45777.png";
#if DEBUG
            return DEFAULT_IMAGE;
#endif
            var urlEscapedName = HttpUtility.UrlEncode(latinPlantname);
            var request = new RestRequest($"/species-list?q={urlEscapedName}&key={API_KEY}");
            try
            {
                var response = await GetClient().GetAsync<SearchPlantResponse>(request);

                if (!response.data.Any())
                {
                    return string.Empty;
                }

                var data = response.data.First();

                if (data.default_image == null)
                {
                    // NOTE: We found the image, but this is outside the free api, so just show a plant icon.
                    return DEFAULT_IMAGE;
                }

                return data.default_image.small_url;
            }
            catch
            {
                return DEFAULT_IMAGE;
            }
        }

    }
}
