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
        private readonly string _projectName = "//section[@data-cy='name']/div";
        private readonly string _addApButton = "//button[contains(text(),'Add AP')]";
        private readonly string _hardwareButton = "//button[@value='hardware']";        
        private readonly string _addAnApButton = "//button[contains(text(),'Add an AP')]";
        private readonly string _idfTab = "//button[contains(text(),'IDF')]";
        private readonly string _addAnIdfButton = "//button[contains(text(),'Add an IDF')]";
        private readonly string _switchNameTextBox = "//input[@id='name']";
        private readonly string _idfNameTextBox = "//input[@id='name-0']";
        private readonly string _noOfPortsTextBox = "//input[@id='numberOfPorts-0']";
        private readonly string _totalPowerTextBox = "//input[@id='totalPowerOutputW-0']";
        private readonly string _switchButton = "//button[contains(text(),'Switch')]";
        private readonly string _addIDFButton = "//button[contains(text(),'Add IDF')]";
        private readonly string _apTypeDropdown = "(//div[@role='combobox'])[2]";
        private readonly string _apVendorDropdown = "(//div[@role='combobox'])[3]"; 
        private readonly string _wifiIcon = "(//div[@class='PinLabels-Label PinLabels-Label_color_dark'])[i]";
        private readonly string _appIcon = "//a[@href='/']";
        private readonly string _projectMenu = "//section[contains(@data-cy, 'project-card-MultiplePolygonPoints')]//div[contains(@class, 'Card-Actions')]/div";
        private readonly string _projectDeleteButton = "(//li[@data-cy='project-card-menu-delete'])[1]";
        private readonly string _projectDeleteConfirmButton = "//button[text() = 'Yes, delete']";
        private readonly string _projectHoverLocator = "//section[contains(@data-cy, 'project-card-MultiplePolygonPoints')]//h3";
        private readonly string _usedPortsLocator = "//input[@id='portsUsed-0']"; 
        private readonly string _freePortsLocator = "//input[@id='freePorts-0']";
        private readonly string _selectButtonLocator = "//button[@value = 'select']";
        private readonly string _idfRowLocator = "//span[text() = 'txt']";
        private readonly string _cancelButton = "(//button[text() = 'Cancel'])[1]";
        private readonly string _idfPlusIcon = "(//button[text() = 'IDF'])[2]";
        private readonly string _eyeIconMap = "(//button[@variant='clear']//*[@size='16'])[3]";
        private readonly string _wiringIcon = "(//button[contains(@class,'MuiIconButton-root')])[25]";
        private readonly string _hardwareIcon = "(//button[contains(@class,'MuiIconButton-root')])[20]";
        private readonly string _LabelsIcon = "(//button[contains(@class,'MuiIconButton-root')])[21]";
        private readonly string _channelsIcon = "(//button[contains(@class,'MuiIconButton-root')])[22]";
        private readonly string _pinsIcon = "(//button[contains(@class,'MuiIconButton-root')])[24]";
        private readonly string _wifiIconLabelAP = "(//div[@class='PinLabels-Label PinLabels-Label_color_dark'])[i]";
        private readonly string _wifiIconChannel = "(//div[@class='PinLabels-Label PinLabels-Label_color_light'])[i]";
        private readonly string _pinsIconText = "//div[text() = 'txt']";
        private readonly string _wallsButton = "//button[@value='createWall']";
        private readonly string _wallTypeValue = "(//span[@color='systemGray'])[i]";
        private readonly string _wallTypeButton = "//*[text() = 'Wall Type' ]";
        private readonly string _meterialTypeDropdown = "//div[@id='material']";
        private readonly string _thicknessInput = "//input[@id='thickness']";
        private readonly string _thicknessDb = "(//input[@type='number'])[3]";        
        private readonly string _heightInput = "//input[@id='height']";
        private readonly string _saveButtonWallsInfo = "//button[text() = 'Save']";



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
            await _page.WaitForTimeoutAsync(10000);
        }

        public async Task DragAndDrop(float x, float y)
        {
            var canvas = page.Locator("canvas");

            var boundingBox = await canvas.BoundingBoxAsync() ?? throw new Exception("Canvas not found");
            float targetX = (float)(boundingBox.X + boundingBox.Width / 2);
            float targetY = (float)(boundingBox.Y + boundingBox.Height / 2);

            await page.Mouse.MoveAsync(targetX - x, targetY - y); 
            await page.Mouse.DownAsync();
            await page.Mouse.UpAsync(); 
            // await page.Mouse.MoveAsync(targetX, targetY); 
            // await page.Mouse.UpAsync(); 
            
        }

        public async Task DrawSquareAsync(float startX, float startY, float sideLength)
        {
            var canvas = page.Locator("canvas");
            var boundingBox = await canvas.BoundingBoxAsync() ?? throw new Exception("Canvas not found");

            float startCanvasX = (float)(boundingBox.X + startX);
            float startCanvasY = (float)(boundingBox.Y + startY);

            // Click to focus on the canvas
            await page.Mouse.ClickAsync(startCanvasX, startCanvasY);
            
            // Move to the starting position and click
            await page.Mouse.MoveAsync(startCanvasX, startCanvasY);
            await page.Mouse.DownAsync();
            await page.Mouse.ClickAsync(startCanvasX, startCanvasY);

            // Draw Right
            float rightX = startCanvasX + sideLength;
            await page.Mouse.MoveAsync(rightX, startCanvasY);
            await page.Mouse.ClickAsync(rightX, startCanvasY);

            // Draw Down
            float bottomY = startCanvasY + sideLength;
            await page.Mouse.MoveAsync(rightX, bottomY);
            await page.Mouse.ClickAsync(rightX, bottomY);

            // Draw Left
            await page.Mouse.MoveAsync(startCanvasX, bottomY);
            await page.Mouse.ClickAsync(startCanvasX, bottomY);

            // Draw Up (Closing the Square)
            await page.Mouse.MoveAsync(startCanvasX, startCanvasY);
            await page.Mouse.ClickAsync(startCanvasX, startCanvasY);

            await page.Mouse.UpAsync();
            // await page.Mouse.DblClickAsync(startCanvasX, startCanvasY);
            await VerifyWallCreated(startCanvasX, startCanvasY);

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

        public async Task VerifyIconOnMap(string index)
        {
            var wifiIconDynamicLocator = Regex.Replace(_wifiIcon, @"\[\s*i\s*\]", $"[{index}]");
            var element = page.Locator(wifiIconDynamicLocator);
            await Assertions.Expect(element).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 50000 });        
        }

        public async Task ClickIdfTab()
        {
            await Helper.Click(_page, _idfTab);
        }

        public async Task ClickAddAnIdfButton()
        {
            await Helper.Click(_page, _addAnIdfButton);
        }

        public async Task ClickSwitchButton()
        {
            await Helper.Click(_page, _switchButton);
        }

        public async Task ClickAddIdfButton()
        {
            await Helper.Click(_page, _addIDFButton);
            await _page.WaitForTimeoutAsync(10000);

        }

        public async Task EnterSwitchName(string switchName)
        {
            await Helper.Fill(_page, _switchNameTextBox, switchName);
        }

        public async Task EnterIdfName(string idfName)
        {
            await Helper.Fill(_page, _idfNameTextBox, idfName);
        }

        public async Task EnterNumberOfPorts(string numberOfPorts)
        {
            await Helper.Fill(_page, _noOfPortsTextBox, numberOfPorts);
        }

        public async Task EnterTotalPower(string totalPower)
        {
            await Helper.Fill(_page, _totalPowerTextBox, totalPower);
        }

        public async Task ClickAppIcon()
        {
            await Helper.Click(_page, _appIcon);
        }

        public async Task ClickSelectButton()
        {
            await Helper.Click(_page, _selectButtonLocator);
        }

        public async Task ClickCancelButton()
        {
            await Helper.Click(_page, _cancelButton);
        }

        public async Task ClickProjectMenu()
        {
            var element = page.Locator(_projectMenu);
            await element.HoverAsync();
            await Task.Delay(3000);
            await element.ClickAsync(new() { Force = true });
        }

        public async Task ClickProjectDelete()
        {
            var element = page.Locator(_projectDeleteButton);
            await element.HoverAsync();
            await Task.Delay(3000);
            await element.ClickAsync(new() { Force = true });
        }

        public async Task ClickConfirmDelete()
        {
            await Helper.Click(_page, _projectDeleteConfirmButton);
        }

         public async Task MouseOverClickCreatedProject()
        {
            var element = page.Locator(_projectHoverLocator);
            await element.HoverAsync();
            await Task.Delay(3000);
        }

        public async Task ClickDroppedIconOnMap(string index)
        {
            var dynamicLocator = Regex.Replace(_wifiIcon, @"\[\s*i\s*\]", $"[{index}]");
            var element = page.Locator(dynamicLocator);
            // await element.HoverAsync();
            // await Task.Delay(3000);
            // await element.ClickAsync(new() { Force = true });

            await element.ClickAsync(new LocatorClickOptions { Force = true });
            await element.ClickAsync(new LocatorClickOptions { Position = new Position { X = 5, Y = 5 } });
        }

        public async Task AssertUsedPortsValue(string expectedValue)
        {
            var usedPortsElement = page.Locator(_usedPortsLocator);
            string? actualValue = await usedPortsElement.GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), $"Expected value '{expectedValue}' but found '{actualValue}'.");
        }

        public async Task AssertFreePortsValue(string expectedValue)
        {
            var usedPortsElement = page.Locator(_freePortsLocator);
            string? actualValue = await usedPortsElement.GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), $"Expected value '{expectedValue}' but found '{actualValue}'.");
        }

        public async Task ClickIdfRecordRow(string idfName)
        {
            var idfNameDyanamicLocator = _idfRowLocator.Replace("txt", idfName);
            var locator = page.Locator(idfNameDyanamicLocator);
            await locator.ClickAsync(new() { Force = true });
        }

        public async Task ClickIdfPlusIcon()
        {
            await Helper.Click(_page, _idfPlusIcon);
        }

        public async Task ClickEyeIcon()
        {
            await Helper.Click(_page, _eyeIconMap);
        }

        public async Task CLickWiringButton()
        {
            await Helper.Click(_page, _wiringIcon);
        }

        public async Task ClickHardwareIconAsync()
        {
            var locator = page.Locator(_hardwareIcon);
            await locator.ClickAsync(new() { Force = true });

        }

        public async Task ClickLabelsIconAsync()
        {
            var locator = page.Locator(_LabelsIcon);
            await locator.ClickAsync(new() { Force = true });
        }

        public async Task ClickChannelsIconAsync()
        {
            var locator = page.Locator(_channelsIcon);
            await locator.ClickAsync(new() { Force = true });
        }

        public async Task ClickPinsIconAsync()
        {
            var locator = page.Locator(_pinsIcon);
            await locator.ClickAsync(new() { Force = true });
        }

        public async Task VerifyWifiIconLabelAPNotOnMapAsync(int index, string labelText)
        {
            var locator = _wifiIconLabelAP.Replace("[i]", $"[{index}]");
            var element = page.Locator(locator);
            string elementText = await element.InnerTextAsync();
            Assert.That(elementText, Is.Not.EqualTo(labelText), $"WiFi Icon Channel at index {index} contains '23' but it should not.");
        }        

        public async Task VerifyWifiIconChannelAsync(int index, string text)
        {
            var locator = _wifiIconChannel.Replace("[i]", $"[{index}]");
            var element = page.Locator(locator);

            string elementText = await element.InnerTextAsync();
            Assert.That(elementText, Is.EqualTo(text), $"WiFi Icon Channel at index {index} contains '23' but it should not.");
            
        }

        public async Task VerifyIconNotOnMap(string index)
        {
            var wifiIconDynamicLocator = Regex.Replace(_wifiIcon, @"\[\s*i\s*\]", $"[{index}]");
            var element = page.Locator(wifiIconDynamicLocator);
            
            Assert.That(await element.IsHiddenAsync(), Is.True, $"Icon at index {index} is still visible on the map.");
        }

        public async Task VerifyPinsNotOnMap(string text)
        {
            var locator = _pinsIconText.Replace("txt", text);
            var element = page.Locator(locator);
            
            Assert.That(await element.IsHiddenAsync(), Is.True, $"Icon at txt {text} is still visible on the map.");
        }

        public async Task ClickWallsButtonAsync()
        {
            await Helper.Click(_page, _wallsButton);
        }

        public async Task ClickWallTypeButtonAsync()
        {
            await Helper.Click(_page, _wallTypeButton);
        }

        public async Task ClickSaveButtonWallsInfoAsync()
        {
            await Helper.Click(_page, _saveButtonWallsInfo);
        }

        public async Task SelectMeterialType(string material)
        {
            await Helper.SelectFromDropDown(_page, _meterialTypeDropdown, material);
        }

        public async Task VerifyWallTypeValue(int index, string value)
        {
            var locator = _wallTypeValue.Replace("[i]", $"[{index}]");
            var element = page.Locator(locator);
            string elementText = await element.InnerTextAsync();
            Assert.That(elementText, Is.EqualTo(value), $"Value at index {index} should be '{value}', but found '{elementText}'.");
        } 

        public async Task FillHeightAsync(string value)
        {
            await Helper.Fill(_page, _heightInput, value);
        }

        public async Task FillThicknessAsync(string value)
        {
            await Helper.Fill(_page, _thicknessInput, value);
        }

        public async Task VerifyThicknessDb(string expectedValue)
        {
            var thicknessDbElement = page.Locator(_thicknessDb);
            string? actualValue = await thicknessDbElement.GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), $"Expected value '{expectedValue}' but found '{actualValue}'.");
        }

      public async Task VerifyWallCreated(float centerX, float centerY)
        {
            string? requestUri = null;
            int statusCode = 0;
            bool isApiCalled = false;

            page.RequestFinished += async (sender, request) =>
            {
                if (request.Url.EndsWith("/pnp/graphql")) 
                {
                    isApiCalled = true;
                    requestUri = request.Url; // Capture the final request URI
                    var response = await request.ResponseAsync();
                    statusCode = response!.Status;
                }
            };

            await page.Mouse.DblClickAsync(centerX, centerY);
            await page.WaitForTimeoutAsync(3000);

            Assert.That(isApiCalled, Is.True, "GraphQL API call was not triggered after double-click.");
            Assert.That(statusCode, Is.EqualTo(200), $"Expected status 200 but got {statusCode}.");

            Console.WriteLine($"GraphQL API Request URI: {requestUri}");
        }

    }




    }

