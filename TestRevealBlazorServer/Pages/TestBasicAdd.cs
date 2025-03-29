using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RevealBlazorServer.Pages;

namespace TestRevealBlazorServer
{
  [Collection("RevealBlazorServer")]
  public class TestBasicAdd
  {
    [Fact]
    public void ViewIsCreated()
    {
      using var ctx = new TestContext();
      ctx.JSInterop.Mode = JSRuntimeMode.Loose;
      ctx.Services.AddIgniteUIBlazor(
        typeof(IgbButtonModule),
        typeof(IgbRippleModule));
      var componentUnderTest = ctx.RenderComponent<BasicAdd>();
      Assert.NotNull(componentUnderTest);
    }
  }
}
