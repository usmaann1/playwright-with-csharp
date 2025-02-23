using Microsoft.Playwright;
using PlaywrightTests.Helpers;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages.Dashboard
{
    public class DashboardPage(IPage page)
    {
        private readonly IPage _page = page;
        private readonly string _importProjectArchieveButton = "//button[text() = 'Import Project Archive']";

        public async Task ClickImportProjectArchieve()
        {
            await Helper.Click(_page, _importProjectArchieveButton);
        }
        
    }
}
