using System.Net.Http.Json;
using RevealBlazorServer.Models.AcmeAnalyticsServer;

namespace RevealBlazorServer.AcmeAnalyticsServer
{
    public class AcmeAnalyticsServerService: IAcmeAnalyticsServerService
    {
        private readonly HttpClient _http;

        public AcmeAnalyticsServerService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<DashboardNames>> GetDashboardNamesList()
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://acmeanalyticsserver.azurewebsites.net/dashboards/names", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DashboardNames>>().ConfigureAwait(false);
            }

            return new List<DashboardNames>();
        }

        public async Task<List<VisualizationChartInfo>> GetVisualizationChartInfoList()
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://acmeanalyticsserver.azurewebsites.net/dashboards/visualizations", UriKind.RelativeOrAbsolute));
            using HttpResponseMessage response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<VisualizationChartInfo>>().ConfigureAwait(false);
            }

            return new List<VisualizationChartInfo>();
        }
    }
}
