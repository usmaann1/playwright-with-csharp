using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Helpers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages.Projects
{
    public class ProjectsPage(IPage page)
    {
        private readonly IPage _page = page;
        private readonly string _projectFileInput = "//input[@type='file']";
        private readonly string _nextButton = "//button[text() = 'Next']";   
        private readonly string _technologyDropdown = "(//div[@role='combobox'])[1]"; 
        private readonly string _vendorDropdown = "(//div[@role='combobox'])[2]"; 
        private readonly string _modelDropdown = "(//div[@role='combobox'])[3]"; 
        private readonly string _projectBuildingTextLocator = "(//section[@data-cy='project-card-MultiplePolygonPoints.kmz']//footer/div/div)[1]";
        private readonly string _projectName = "//section[@data-cy='name']";
        private readonly string _addApButton = "//button[contains(text(),'Add AP')]";
        private readonly string _hardwareButton = "//button[@value='hardware']";
        private readonly string _addAnApButton = "//button[contains(text(),'Add an AP')]";
        private readonly string _apTypeDropdown = "(//div[@role='combobox'])[2]";
        private readonly string _apVendorDropdown = "(//div[@role='combobox'])[3]"; 
        private readonly string _wifiIcon = "(//div[@class='PinLabels-Label PinLabels-Label_color_dark'])[i]";


        public async Task ClickNextButton()
        {
            await Helper.Click(_page, _nextButton);
        }

        public async Task ClickCreatedProject(string projectName)
        {
            var projectNameLocator = _projectName.Replace("name", projectName);
            await Helper.Click(_page, projectNameLocator);
        }

        public async Task UploadProjectFile(string filePath)
        {
           await Helper.UploadFile(_page, _projectFileInput, filePath);
            
        }
        public async Task SelectTechnology(string tech)
        {
            await Helper.SelectFromDropDown(_page, _technologyDropdown, tech);
        }

        public async Task SelectVendor(string vendor)
        {
            await Helper.SelectFromDropDown(_page, _vendorDropdown, vendor);
        }

        public async Task SelectModel(string model)
        {
            await Helper.SelectFromDropDown(_page, _modelDropdown, model);
        }

        public async Task VerifyProjectBuilding()
        {
            var element = page.Locator(_projectBuildingTextLocator);
            Assert.That(await element.IsVisibleAsync(), Is.True);
        }

        public async Task VerifyProjectNameAfterOpening(string name)
        {
            string containsLocator = $"(//span[contains(text(), '{name}')])[1]";
            var element = page.Locator(containsLocator);
            await Assertions.Expect(element).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 50000 });        
        }

        public async Task ClickAddApButton()
        {
            await Helper.Click(_page, _addApButton);
        }

        public async Task DragAndDropAp(float x, float y)
        {
            var canvas = page.Locator("canvas");

            var boundingBox = await canvas.BoundingBoxAsync() ?? throw new Exception("Canvas not found");
            float targetX = (float)(boundingBox.X + boundingBox.Width / 2);
            float targetY = (float)(boundingBox.Y + boundingBox.Height / 2);

            await page.Mouse.MoveAsync(targetX - x, targetY - y); 
            await page.Mouse.DownAsync();
            await page.Mouse.MoveAsync(targetX, targetY); 
            await page.Mouse.UpAsync(); 
            
        }

        public async Task ClickHardwareButton()
        {
            await Helper.Click(_page, _hardwareButton);
        }

        public async Task ClickAddAnApButton()
        {
            await Helper.Click(_page, _addAnApButton);
        }

        public async Task SelectApType(string type)
        {
            await Helper.SelectFromDropDown(_page, _apTypeDropdown, type);
        }

        public async Task SelectApVendor(string vendor)
        {
            await Helper.SelectFromDropDown(_page, _apVendorDropdown, vendor);
        }

          public async Task VerifyWifiIconOnMap(string index)
        {
            var wifiIconDynamicLocator = Regex.Replace(_wifiIcon, @"\[\s*i\s*\]", $"[{index}]");
            var element = page.Locator(wifiIconDynamicLocator);
            await Assertions.Expect(element).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 50000 });        
        }
        }




    }

