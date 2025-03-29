using RevealBlazorServer.Models.AcmeAnalyticsServer;

namespace RevealBlazorServer.AcmeAnalyticsServer
{
    public interface IAcmeAnalyticsServerService
    {
        Task<List<DashboardNames>> GetDashboardNamesList();
        Task<List<VisualizationChartInfo>> GetVisualizationChartInfoList();
    }
}
