using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RevealBlazorServer.Pages;
using RevealBlazorServer.AcmeAnalyticsServer;

namespace TestRevealBlazorServer
{
  [Collection("RevealBlazorServer")]
  public class TestVizCatalog
  {
    [Fact]
    public void ViewIsCreated()
    {
      using var ctx = new TestContext();
      ctx.JSInterop.Mode = JSRuntimeMode.Loose;
      ctx.Services.AddIgniteUIBlazor(
        typeof(IgbListModule),
        typeof(IgbAvatarModule));
      ctx.Services.AddScoped<IAcmeAnalyticsServerService>(sp => new MockAcmeAnalyticsServerService());
      var componentUnderTest = ctx.RenderComponent<VizCatalog>();
      Assert.NotNull(componentUnderTest);
    }
  }
}
