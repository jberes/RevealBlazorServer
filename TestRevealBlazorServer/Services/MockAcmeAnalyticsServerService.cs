using RevealBlazorServer.Models.AcmeAnalyticsServer;

namespace RevealBlazorServer.AcmeAnalyticsServer
{
    public class MockAcmeAnalyticsServerService : IAcmeAnalyticsServerService
    {
        public Task<List<DashboardNames>> GetDashboardNamesList()
        {
            return Task.FromResult<List<DashboardNames>>(new());
        }

        public Task<List<VisualizationChartInfo>> GetVisualizationChartInfoList()
        {
            return Task.FromResult<List<VisualizationChartInfo>>(new());
        }
    }
}
