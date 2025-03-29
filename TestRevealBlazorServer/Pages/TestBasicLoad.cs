using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RevealBlazorServer.Pages;
using RevealBlazorServer.AcmeAnalyticsServer;

namespace TestRevealBlazorServer
{
  [Collection("RevealBlazorServer")]
  public class TestBasicLoad
  {
    [Fact]
    public void ViewIsCreated()
    {
      using var ctx = new TestContext();
      ctx.JSInterop.Mode = JSRuntimeMode.Loose;
      ctx.Services.AddIgniteUIBlazor(
        typeof(IgbComboModule));
      ctx.Services.AddScoped<IAcmeAnalyticsServerService>(sp => new MockAcmeAnalyticsServerService());
      var componentUnderTest = ctx.RenderComponent<BasicLoad>();
      Assert.NotNull(componentUnderTest);
    }
  }
}
