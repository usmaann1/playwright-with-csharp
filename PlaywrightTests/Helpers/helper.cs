using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Helpers
{
    public static class Helper
    {
        public static async Task Click(IPage page, string locator, int timeout = 50000)
        {
            await Task.Delay(AppConfig.HeadlessWait!);
            var element = page.Locator(locator);
            try
            {
        
                await element.ClickAsync(new LocatorClickOptions
                {
                    Timeout = timeout
                });
            }
            catch (PlaywrightException)
            {
                await element.ClickAsync(new LocatorClickOptions
                {
                    Timeout = timeout,
                    Force = true
                });
            }
        }
        public static async Task Fill(IPage page, string locator, string value, int timeout = 10000)
        {
            await Task.Delay(AppConfig.HeadlessWait!);

            var element = page.Locator(locator);
            try
            {
                await element.FillAsync(value, new LocatorFillOptions
                {
                    Timeout = timeout
                });
            }
            catch (PlaywrightException)
            {
                await element.ClearAsync(new LocatorClearOptions
                {
                    Timeout = timeout
                });

                await element.FillAsync(value, new LocatorFillOptions
                {
                    Timeout = timeout
                });
            }
        }
        public static async Task UploadFile(IPage page, string fileInputLocator, string filePath, int timeout = 5000)
        {
            var fileInput = page.Locator(fileInputLocator);
            string path = GetProjectRoot()+filePath;
            await fileInput.SetInputFilesAsync(path, new LocatorSetInputFilesOptions
            {
                Timeout = timeout
            });
        }
        public static async Task SelectFromDropDown(IPage page, string dropdownLocator, string dropdownValue, string dropdownOptionLocator = "//li[@role='option']" )
        {
            await Task.Delay(AppConfig.HeadlessWait!);

            await page.Locator(dropdownLocator).ClickAsync();

            var options = page.Locator(dropdownOptionLocator);
            var optionCount = await options.CountAsync();

            for (int i = 0; i < optionCount; i++)
            {
                var optionText = await options.Nth(i).InnerTextAsync();

                if (optionText.Trim() == dropdownValue)
                {
                    await options.Nth(i).ClickAsync();
                    return; 
                }
            }
            throw new System.Exception($"Dropdown option '{dropdownValue}' not found.");
        }
        public static string GetProjectRoot()
        {
            return Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        }
    }
}
