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
        private readonly string _obstructionsButton = "//button[@value='obstructions']";
        private readonly string _wallTypeValue = "(//span[@color='systemGray'])[i]";
        private readonly string _wallTypeButton = "//*[text() = 'Wall Type' ]";
        private readonly string _meterialTypeDropdown = "//div[@id='material']";
        private readonly string _thicknessInput = "//input[@id='thickness']";
        private readonly string _thicknessDb = "(//input[@type='number'])[3]";        
        private readonly string _heightInput = "//input[@id='height']";
        private readonly string _saveButtonWallsInfo = "//button[text() = 'Save']";
        private readonly string _tenFtWallThickness = "//span[text() = '10ft']";
        private readonly string _tenFtWallThicknessObstruction = "(//span[text() = '10ft'])[4]";
        private readonly string _wallKebabMenu = "(//button[@variant='clear'])[4]";
        private readonly string _selectAllButton = "//span[text() = 'Select all']";
        private readonly string _deleteWallButton = "(//button//*[@stroke-linejoin='round'])[27]";
        private readonly string _deleteWallsAlertButton = "//button[text() = 'Delete']";
        private readonly string _addObstructionButton = "//button[text() = 'Add Obstruction']"; 
        private readonly string _rectangleButton = "//button[@value='rectangle']"; 
        private readonly string _obstructionMaterialDropdown = "//div[@id='mui-component-select-material']"; 
        private readonly string _roofThicknessInput = "//input[@id='roofThickness']"; 
        private readonly string _addTypeButton = "//button[text() = 'Add type']"; 
        private readonly string _obstructionKebabMenuButton = "(//button[contains(@class,'MuiButtonBase-root MuiIconButton-root')])[13]";
        private readonly string _obstructionDeleteButton = "//span[text() = 'Delete']";
        private readonly string _createObstructionKebabBrickMenu = "(//div[@class='GroupItem-Suffix']//*[@stroke-linejoin='round'])[150]";
        private readonly string _antennasTab = "//button[text() = 'Antennas']";
        private readonly string _aPsTab = "//button[text() = 'APs']";
        private readonly string _firstApRecordRow = "(//span[text() = '1'])[2]";
        private readonly string _antennaTypeDropDown = "(//div[@role='combobox'])[1]";
        private readonly string _antennaTypeDropDownSecondOption = "(//li[@role='option'])[2]";
        private readonly string _antennaMenuPanel = "//div[@data-popper-placement='bottom']";
        private readonly string _antennaMenuPanelCrossIcon = "//div[@data-popper-placement='bottom']//button";
        private readonly string _bandDropDown = "//div[@id='band']";
        private readonly string _closeApInfoPanel = "(//button[contains(@class,'MuiIconButton-root')])[15]";
        private readonly string _gainValue = "//input[@id='gain]";
        private readonly string _measureButton = "//button[@value='measure']";    
        private readonly string _measureGraphPanel = "(//div[contains(@class,'MuiBox-root')])//*[@height='250']";   
        private readonly string _measureTextOnCanvas = "//*[text() = ' Click to start measurement ']";   
        private readonly string _lineOfSightPanel= "//span[text() = 'Line of Sight']";   
        private readonly string _panelLatitude= "(//input[@type='number'])[3]";   
        private readonly string _panelLogitude= "(//input[@type='number'])[4]";   
        private readonly string _canvasLatitude= "//span[contains(text(), 'Latitude:')]";   
        private readonly string _canvasLongitude= "//span[contains(text(), 'Longitude:')]";
        private readonly string _measureGraphPanelCross = "(//button[contains(@class,'MuiButtonBase-root')])[26]";   
        private readonly string _lineOfSightPanelCross = "(//button[contains(@class,'MuiButtonBase-root')])[26]";   
        private readonly string _dsmButton = "//button[@value='dsm']";    
        private readonly string _treeTypeDropdown = "//div[@id = 'name'] ";
        private readonly string _treeTrunkVsCanopyValue = "//input[@id = 'bottomHeightPct']";
        private readonly string _summaryButton = "(//button[contains(@class, 'MuiIconButton-sizeSmall')])[10]";        
        private readonly string _projectNameOnTop = "(//a[@href='/']/parent::div//span)[1]";      
        private readonly string _layoutNameOnTop = "(//a[@href='/']/parent::div//span)[1]";   
        private readonly string _projectNameSummary = "(//div[text()='Project name']/parent::div//div)[2]";      
        private readonly string _layoutNameSummary = "(//div[text()='Layout name']/parent::div//div)[2]";      
        private readonly string _summaryWifiRecordsRow = "//span[text()='WiFi']/parent::div/div[@class='css-pnfj59']";      
        private readonly string _summaryMeterialsTab = "(//button[@role='tab'])[2]";   
        private readonly string _summaryMeterialsWifiModle = "(//span[text()='Access Points']/parent::div/parent::p/parent::*//span)[8]";     
        private readonly string _summaryMeterialsWifiCount = "(//span[text()='Access Points']/parent::div/parent::p/parent::*//span)[9]";     
        private readonly string _summaryMeterialsIDFModel = "(//span[text()='IDFs']/parent::div/parent::p/parent::*//span)[8]";     
        private readonly string _summaryMeterialsIDFCount = "(//span[text()='IDFs']/parent::div/parent::p/parent::*//span)[9]";     
        private readonly string _apParentKebabButton = "(//button[@variant='clear'])[4]";     
        private readonly string _editConfigButton = "//span[text() = 'Edit config']";
        private readonly string _updateApButton = "//button[contains(text() , 'Update')]";
        private readonly string _apRecordSidePanelCrossButton = "((//span[text() = 'txt'])[3]/parent::*/parent::*/parent::*//button)[1]";
        private readonly string _apType = "//div[@id= 'type']";
        private readonly string _vendorType = "(//label[text() = 'Vendor & model']/parent::*//div[@role='combobox'])[1]";
        private readonly string _childApRecordRow = "//span[text()='WiFi']/parent::*//span[contains(text(), 'layout')]";
        private readonly string _apConfigTab = "//button[text()='Config']";
        private readonly string _apConfigMountingDropdown = "//div[@id='mounting']";
        private readonly string _apConfigLoss = "//input[@id='lossTerm']";
        private readonly string _apConfigHeight = "//input[@id='height']";
        private readonly string _apConfigAboveRoof = "//input[@id='zAboveRoof']";
        private readonly string _apConfigAboveRoofCheckbox = "//input[@name='setAboveRoof']";
        private readonly string _apConfigChannelWidthDropdown = "(//div[contains(@id, 'channelWidth')])[2]";
        private readonly string _apConfigTransmitPower = "(//input[contains(@id, 'transmitPower')])[2]";
        private readonly string _apConfigModulutionSchemaDropdown = "(//div[contains(@id, 'modulation')])[2]";
        private readonly string _apConfigMimoDropdown = "(//div[contains(@id, 'mimo')])[2]";
        private readonly string _apConfigRequiredPOEPower = "(//input[contains(@id, 'requiredPower')])[1]";


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

            float startCanvasX =  (float)(boundingBox.X + boundingBox.Width / 2);
            float startCanvasY = (float)(boundingBox.Y + boundingBox.Height / 2);

            // Click to focus on the canvas
            await page.Mouse.ClickAsync(startCanvasX, startCanvasY);
            
            // Move to the starting position and click
            await page.Mouse.MoveAsync(startCanvasX-startX, startCanvasY-startY);
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

        public async Task DrawStraightLineAsync(float offsetX1, float offsetY1, float offsetX2, float offsetY2)
        {
            var canvas = page.Locator("canvas");
            var boundingBox = await canvas.BoundingBoxAsync() ?? throw new Exception("Canvas not found");

            // Calculate the center of the canvas
            float centerX = (float)(boundingBox.X + boundingBox.Width / 2);
            float centerY = (float)(boundingBox.Y + boundingBox.Height / 2);

            // Calculate the start and end points relative to the center
            float startX = centerX + offsetX1;
            float startY = centerY + offsetY1;
            float endX = centerX + offsetX2;
            float endY = centerY + offsetY2;

            // Click to focus on the canvas
            await page.Mouse.ClickAsync(centerX, centerY);

            // Move to the start position and click
            await page.Mouse.MoveAsync(startX, startY);
            await page.Mouse.DownAsync();

            // Move to the end position and click
            await page.Mouse.MoveAsync(endX, endY);
            await page.Mouse.UpAsync();

            await page.Mouse.ClickAsync(endX, endY);

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
            string? actualValue = await usedPortsElement.GetAttributeAsync("value" , new() { Timeout = 50000 });
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
            string? actualValue = await thicknessDbElement.GetAttributeAsync("value" , new() { Timeout = 50000 });
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

        public async Task VerifyTenFtWallThicknessDisplayed()
        {
            var element = page.Locator(_tenFtWallThickness);
            Assert.That(await element.IsVisibleAsync(), Is.True, "10ft Wall Thickness is not displayed.");
        }

        public async Task VerifyTenFtWallThicknessDisplayedObstruction()
        {
            var element = page.Locator(_tenFtWallThicknessObstruction);
            Assert.That(await element.IsVisibleAsync(), Is.True, "10ft Wall Thickness is not displayed.");
        }

        public async Task VerifyCreatedObstructionNotDiplayed()
        {
            var element = page.Locator(_tenFtWallThicknessObstruction);
            Assert.That(await element.IsVisibleAsync(), Is.False, "10ft Wall Thickness is not displayed.");
        }

        public async Task VerifyCreatedWallNotDisplayed()
        {
            var element = page.Locator(_tenFtWallThickness);
            Assert.That(await element.IsVisibleAsync(), Is.False, "10ft Wall Thickness is displayed but it should not be.");
        }

        public async Task ClickWallKebabMenu()
        {
            await Helper.Click(_page, _wallKebabMenu);
        }

        public async Task ClickSelectAllButton()
        {
            await Helper.Click(_page, _selectAllButton);
        }

        public async Task ClickDeleteWallButton()
        {
            await Helper.Click(_page, _deleteWallButton);
        }

        public async Task ClickDeleteWallsAlertButton()
        {
            await Helper.Click(_page, _deleteWallsAlertButton);
        }

        public async Task ClickObstructionsButton()
        {
            await Helper.Click(_page, _obstructionsButton);
        }

        public async Task ClickAddObstructionButton()
        {
            await Helper.Click(_page, _addObstructionButton);
        }

        public async Task ClickRectangleButton()
        {
            await Helper.Click(_page, _rectangleButton);
        }

        public async Task FillRoofThicknessInput(string value)
        {
            await Helper.Fill(_page, _roofThicknessInput, value);
        }

        public async Task SelectObstructionMaterial(string type)
        {
            await Helper.SelectFromDropDown(_page, _obstructionMaterialDropdown, type);
        }

        public async Task ClickAddTypeButton()
        {
            await Helper.Click(_page, _addTypeButton);
        }

        public async Task HoverAndClickObstructionKebabMenuButton()
        {
            var locator = page.Locator(_obstructionKebabMenuButton);
            await locator.ClickAsync();
        }

        public async Task ClickObstructionDeleteButton()
        {
            await Helper.Click(_page, _obstructionDeleteButton);
        }

        public async Task HoverAndClickCreateObstructionKebabBrickMenu()
        {
            var element = _page.Locator(_createObstructionKebabBrickMenu);
            await element.HoverAsync();
            await element.ClickAsync();
        }

        public async Task ClickAPsTab()
        {
            await Helper.Click(_page, _aPsTab);
        }

        public async Task ClickFirstApRecordRow()
        {
            await Helper.Click(_page, _firstApRecordRow);
        }

        public async Task ClickAntennasTab()
        {
            await Helper.Click(_page, _antennasTab);
        }

        public async Task ClickAntennaTypeDropDown()
        {
            await Helper.Click(_page, _antennaTypeDropDown);
        }

        public async Task HoverOverAntennaTypeDropDownSecondOption()
        {
            var element = _page.Locator(_antennaTypeDropDownSecondOption);
            await element.HoverAsync();
        }

        public async Task VerifyAntennaMenuPanelDisplayed()
        {
            var element = _page.Locator(_antennaMenuPanel);
            Assert.That(await element.IsVisibleAsync(), Is.True, "Antenna Menu Panel is not displayed.");
        }

        public async Task VerifyAntennaMenuPanelNotDisplayed()
        {
            var element = _page.Locator(_antennaMenuPanel);
            Assert.That(await element.IsVisibleAsync(), Is.False, "Antenna Menu Panel is not displayed.");
        }

        public async Task VerifyGainValue(string expectedValue)
        {
            var gainValueElement = page.Locator("//input[@id='gain']");
            string? actualValue = await gainValueElement.GetAttributeAsync("value" , new() { Timeout = 50000 });
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), $"Expected value '{expectedValue}' but found '{actualValue}'.");
        }

        public async Task SelectBand(string band)
        {
            await Helper.SelectFromDropDown(_page, _bandDropDown, band);
        }

        public async Task CloseAntennaMenuPannel()
        {
            await Helper.Click(_page, _antennaMenuPanelCrossIcon);
        }

        public async Task CloseAPInfoPanel()
        {
            await Helper.Click(_page, _closeApInfoPanel);
        }

        public async Task ClickMeasure()
        {
            await Helper.Click(_page, _measureButton);
        }

        public async Task VerifyMeasureGraphPanelIsDisplayed()
        {
            var element = page.Locator(_measureGraphPanel);
            Assert.That(await element.IsVisibleAsync(), Is.True, "Measure Graph Panel is not displayed.");
        }

        public async Task VerifyMeasureGraphPanelIsNotDisplayed()
        {
            var element = page.Locator(_measureGraphPanel);
            Assert.That(await element.IsVisibleAsync(), Is.False, "Measure Graph Panel is displayed, but it should not be.");
        }

        public async Task VerifyMeasureTextOnCanvasAsync()
        {
            var element = page.Locator(_measureTextOnCanvas);
            Assert.That(await element.IsVisibleAsync(), Is.True, "Measure text 'Click to start measurement' is not displayed on the canvas.");
        }

        public async Task VerifyLineOfSightPanelDisplayedAsync()
        {
            var element = page.Locator(_lineOfSightPanel);
            Assert.That(await element.IsVisibleAsync(), Is.True, "The 'Line of Sight' panel is not displayed.");
        }

        public async Task VerifyLineOfSightPanelNotDisplayedAsync()
        {
            var element = page.Locator(_lineOfSightPanel);
            Assert.That(await element.IsVisibleAsync(), Is.False, "The 'Line of Sight' panel is not displayed.");
        }

        public async Task VerifyLatitudeLongitudeMatchAsync()
        {
            // Get values from panel inputs
            var panelLatitudeElement = page.Locator(_panelLatitude);
            var panelLongitudeElement = page.Locator(_panelLogitude);

            string? panelLatitudeText = await panelLatitudeElement.GetAttributeAsync("value");
            string? panelLongitudeText = await panelLongitudeElement.GetAttributeAsync("value");

            Assert.That(panelLatitudeText, Is.Not.Null.Or.Empty, "Panel Latitude value is missing.");
            Assert.That(panelLongitudeText, Is.Not.Null.Or.Empty, "Panel Longitude value is missing.");

            // Get text from canvas elements
            var canvasLatitudeElement = page.Locator(_canvasLatitude);
            var canvasLongitudeElement = page.Locator(_canvasLongitude);

            string canvasLatitudeText = await canvasLatitudeElement.InnerTextAsync();
            string canvasLongitudeText = await canvasLongitudeElement.InnerTextAsync();

            // Extract the numeric values from the text
            string canvasLatitudeStr = canvasLatitudeText.Replace("Latitude: ", "").Trim();
            string canvasLongitudeStr = canvasLongitudeText.Replace("Longitude: ", "").Trim();

            // Convert to double and round to 2 decimal places
            double panelLatitude = Math.Round(Convert.ToDouble(panelLatitudeText), 2);
            double panelLongitude = Math.Round(Convert.ToDouble(panelLongitudeText), 2);
            double canvasLatitude = Math.Round(Convert.ToDouble(canvasLatitudeStr), 2);
            double canvasLongitude = Math.Round(Convert.ToDouble(canvasLongitudeStr), 2);

            Assert.That(canvasLatitude, Is.EqualTo(panelLatitude), 
                $"Mismatch: Expected Latitude '{panelLatitude}', but found '{canvasLatitude}'.");
            Assert.That(canvasLongitude, Is.EqualTo(panelLongitude), 
                $"Mismatch: Expected Longitude '{panelLongitude}', but found '{canvasLongitude}'.");
        }

        public async Task CloseGraphPanel()
        {
            await Helper.Click(_page, _measureGraphPanelCross);
        }

        public async Task CloseLineOfSightPanel()
        {
            await Helper.Click(_page, _lineOfSightPanelCross);
        }

        public async Task ClickDsm()
        {
            await Helper.Click(_page, _dsmButton);
        }

        public async Task ClickSummary()
        {
            await Helper.Click(_page, _summaryButton);
        }

        public async Task VerifyTreeTrunkCanopyValue(string expectedValue)
        {   
            await Task.Delay(3000);
            var element = page.Locator(_treeTrunkVsCanopyValue);
            string? actualValue = await element.GetAttributeAsync("value", new() { Timeout = 50000 });
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), $"Expected value '{expectedValue}' but found '{actualValue}'.");
        }

        public async Task SelectTreeType(string type)
        {
            await Helper.SelectFromDropDown(_page, _treeTypeDropdown, type);
        }

        public async Task VerifyProjectAndLayoutNamesAsync()
        {
            await Task.Delay(3000);
            string projectNameOnTop = await page.Locator(_projectNameOnTop).InnerTextAsync();
            string projectNameSummary = await page.Locator(_projectNameSummary).InnerTextAsync();
            string layoutNameOnTop = await page.Locator(_layoutNameOnTop).InnerTextAsync();
            string layoutNameSummary = await page.Locator(_layoutNameSummary).InnerTextAsync();

            // Remove " layout" from layoutNameSummary before comparison
            layoutNameSummary = layoutNameSummary.Replace(" layout", "").Trim();

            Assert.That(projectNameOnTop, Is.EqualTo(projectNameSummary), 
                $"Mismatch: Expected Project Name '{projectNameSummary}', but found '{projectNameOnTop}'.");

            Assert.That(layoutNameOnTop, Is.EqualTo(layoutNameSummary), 
                $"Mismatch: Expected Layout Name '{layoutNameSummary}', but found '{layoutNameOnTop}'.");
        }

        public async Task VerifyWifiRecordsNumberSummary(int noOfRecords)
        {
            var wifiRecords = await page.Locator(_summaryWifiRecordsRow).CountAsync();
            Assert.That(wifiRecords, Is.EqualTo(noOfRecords), 
                $"Expected {noOfRecords} WiFi records, but found {wifiRecords}.");
        }

        public async Task VerifySummaryMaterialsWifiModelAsync(string expectedModel)
        {
            var element = page.Locator(_summaryMeterialsWifiModle);
            string actualModel = await element.InnerTextAsync();

            Assert.That(actualModel, Is.EqualTo(expectedModel), 
                $"Mismatch: Expected WiFi Model '{expectedModel}', but found '{actualModel}'.");
        }

        public async Task VerifySummaryMaterialsWifiCountAsync(string expectedCount)
        {
            var element = page.Locator(_summaryMeterialsWifiCount);
            string actualCount = await element.InnerTextAsync();

            Assert.That(actualCount, Is.EqualTo(expectedCount), 
                $"Mismatch: Expected WiFi Model '{expectedCount}', but found '{actualCount}'.");
        }

        public async Task ClickMaterialsTab()
        {
            await Helper.Click(_page, _summaryMeterialsTab);
        }

        public async Task VerifySummaryMaterialsIDFModelAsync(string expectedModel)
        {
            var element = page.Locator(_summaryMeterialsIDFModel);
            string actualModel = await element.InnerTextAsync();

            Assert.That(actualModel, Is.EqualTo(expectedModel), 
                $"Mismatch: Expected WiFi Model '{expectedModel}', but found '{actualModel}'.");
        }

        public async Task VerifySummaryMaterialsIDFCountAsync(string expectedCount)
        {
            var element = page.Locator(_summaryMeterialsIDFCount);
            string actualCount = await element.InnerTextAsync();

            Assert.That(actualCount, Is.EqualTo(expectedCount), 
                $"Mismatch: Expected WiFi Model '{expectedCount}', but found '{actualCount}'.");
        }

        public async Task ClickApParentKebabButtonAsync()
        {
            await page.Locator(_apParentKebabButton).ClickAsync();
        }

        public async Task ClickEditConfigButtonAsync()
        {
            await page.Locator(_editConfigButton).ClickAsync();
        }

        public async Task ClickUpdateApButtonAsync()
        {
            await page.Locator(_updateApButton).ClickAsync();
        }

        public async Task ClickApRecordSidePanelCrossButtonAsync(string text)
        {
            string dynamicLocator = _apRecordSidePanelCrossButton.Replace("txt", text);
            await page.Locator(dynamicLocator).ClickAsync();
        }

        public async Task VerifyApTypeAsync(string expectedValue)
        {
            var element = page.Locator(_apType);
            string actualValue = await element.InnerTextAsync();
            Assert.That(actualValue, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected AP Type '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyVendorTypeAsync(string expectedValue)
        {
            var element = page.Locator(_vendorType);
            string actualValue = await element.InnerTextAsync();
            Assert.That(actualValue, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Vendor Type '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task ClickChildAPRecordRow()
        {
            await page.Locator(_childApRecordRow).ClickAsync();
        }

        public async Task ClickConfigTab()
        {
            await page.Locator(_apConfigTab).ClickAsync();
        }

        public async Task ClickApConfigAboveRoofCheckboxAsync()
        {
            await page.Locator(_apConfigAboveRoofCheckbox).ClickAsync();
        }

        public async Task FillApConfigLossAsync(string value)
        {
            await Helper.Fill(page, _apConfigLoss, value);
        }

        public async Task FillApConfigHeightAsync(string value)
        {
            await Helper.Fill(page, _apConfigHeight, value);
        }

        public async Task FillApConfigAboveRoofAsync(string value)
        {
            await Helper.Fill(page, _apConfigAboveRoof, value);
        }

        public async Task FillApConfigTransmitPowerAsync(string value)
        {
            await Helper.Fill(page, _apConfigTransmitPower, value);
        }

        public async Task FillApConfigRequiredPOEPowerAsync(string value)
        {
            await Helper.Fill(page, _apConfigRequiredPOEPower, value);
        }

        public async Task SelectApConfigMountingAsync(string option)
        {
            await Helper.SelectFromDropDown(page, _apConfigMountingDropdown, option);
        }

        public async Task SelectApConfigChannelWidthAsync(string option)
        {
            await Helper.SelectFromDropDown(page, _apConfigChannelWidthDropdown, option);
        }

        public async Task SelectApConfigModulationSchemaAsync(string option)
        {
            await Helper.SelectFromDropDown(page, _apConfigModulutionSchemaDropdown, option);
        }

        public async Task SelectApConfigMimoAsync(string option)
        {
            await Helper.SelectFromDropDown(page, _apConfigMimoDropdown, option);
        }

        public async Task VerifyApConfigMountingAsync(string expectedValue)
        {
            string actualValue = await page.Locator(_apConfigMountingDropdown).InnerTextAsync();
            Assert.That(actualValue.Trim(), Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Mounting '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigLossAsync(string expectedValue)
        {
            string? actualValue = await page.Locator(_apConfigLoss).GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Loss '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigHeightAsync(string expectedValue)
        {
            string? actualValue = await page.Locator(_apConfigHeight).GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Height '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigAboveRoofAsync(string expectedValue)
        {
            string? actualValue = await page.Locator(_apConfigAboveRoof).GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Above Roof '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigChannelWidthAsync(string expectedValue)
        {
            string actualValue = await page.Locator(_apConfigChannelWidthDropdown).InnerTextAsync();
            Assert.That(actualValue.Trim(), Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Channel Width '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigTransmitPowerAsync(string expectedValue)
        {
            string? actualValue = await page.Locator(_apConfigTransmitPower).GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Transmit Power '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigModulationSchemaAsync(string expectedValue)
        {
            string actualValue = await page.Locator(_apConfigModulutionSchemaDropdown).InnerTextAsync();
            Assert.That(actualValue.Trim(), Is.EqualTo(expectedValue), 
                $"Mismatch: Expected Modulation Schema '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigMimoAsync(string expectedValue)
        {
            string actualValue = await page.Locator(_apConfigMimoDropdown).InnerTextAsync();
            Assert.That(actualValue.Trim(), Is.EqualTo(expectedValue), 
                $"Mismatch: Expected MIMO '{expectedValue}', but found '{actualValue}'.");
        }

        public async Task VerifyApConfigRequiredPOEPowerAsync(string expectedValue)
        {
            string? actualValue = await page.Locator(_apConfigRequiredPOEPower).GetAttributeAsync("value");
            Assert.That(actualValue ?? string.Empty, Is.EqualTo(expectedValue), 
                $"Mismatch: Expected POE Power '{expectedValue}', but found '{actualValue}'.");
        }
        
        public async Task VerifyApConfigAboveRoofCheckedAsync(bool expectedChecked)
        {
            bool isChecked = await page.Locator(_apConfigAboveRoofCheckbox).IsCheckedAsync();
            Assert.That(isChecked, Is.EqualTo(expectedChecked), 
                $"Mismatch: Expected Above Roof checkbox to be '{expectedChecked}', but found '{isChecked}'.");
        }


    }




}

