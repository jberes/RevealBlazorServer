using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RevealBlazorServer.Pages;
using RevealBlazorServer.NorthwindCloud;

namespace TestRevealBlazorServer
{
  [Collection("RevealBlazorServer")]
  public class TestFilterApi
  {
    [Fact]
    public void ViewIsCreated()
    {
      using var ctx = new TestContext();
      ctx.JSInterop.Mode = JSRuntimeMode.Loose;
      ctx.Services.AddIgniteUIBlazor(
        typeof(IgbListModule),
        typeof(IgbAvatarModule));
      ctx.Services.AddScoped<INorthwindCloudService>(sp => new MockNorthwindCloudService());
      var componentUnderTest = ctx.RenderComponent<FilterApi>();
      Assert.NotNull(componentUnderTest);
    }
  }
}
