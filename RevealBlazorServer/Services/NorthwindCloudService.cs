using System.Net.Http.Json;
using RevealBlazorServer.Models.NorthwindCloud;

namespace RevealBlazorServer.NorthwindCloud
{
    public class NorthwindCloudService: INorthwindCloudService
    {
        private readonly HttpClient _http;

        public NorthwindCloudService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EmployeeStringfAnonymousType9>> GetEmployeeStringfAnonymousType9List()
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://northwindcloud.azurewebsites.net/api/employees", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<EmployeeStringfAnonymousType9>>().ConfigureAwait(false);
            }

            return new List<EmployeeStringfAnonymousType9>();
        }
    }
}
