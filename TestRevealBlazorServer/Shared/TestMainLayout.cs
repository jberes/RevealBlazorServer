using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RevealBlazorServer.Shared;

namespace TestRevealBlazorServer
{
  [Collection("RevealBlazorServer")]
  public class TestMainLayout
  {
    [Fact]
    public void ViewIsCreated()
    {
      using var ctx = new TestContext();
      ctx.JSInterop.Mode = JSRuntimeMode.Loose;
      ctx.Services.AddIgniteUIBlazor(
        typeof(IgbNavbarModule),
        typeof(IgbIconButtonModule),
        typeof(IgbRippleModule),
        typeof(IgbNavDrawerModule));
      var componentUnderTest = ctx.RenderComponent<MainLayout>();
      Assert.NotNull(componentUnderTest);
    }
  }
}
