namespace RevealBlazorServer.State
{
    public class StateService: IStateService
    {
        public string RevealServer { get; set; } = "https://acmeanalyticsserver.azurewebsites.net/";
    }
}
