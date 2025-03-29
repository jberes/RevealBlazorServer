using RevealBlazorServer.Models.NorthwindCloud;

namespace RevealBlazorServer.NorthwindCloud
{
    public class MockNorthwindCloudService : INorthwindCloudService
    {
        public Task<List<EmployeeStringfAnonymousType9>> GetEmployeeStringfAnonymousType9List()
        {
            return Task.FromResult<List<EmployeeStringfAnonymousType9>>(new());
        }
    }
}
