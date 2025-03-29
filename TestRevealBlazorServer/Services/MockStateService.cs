namespace RevealBlazorServer.State
{
    public class MockStateService : IStateService
    {
        public string RevealServer { get; set; } = "https://acmeanalyticsserver.azurewebsites.net/";
    }
}
