using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Pages.Dashboard;
using PlaywrightTests.Pages.Login;
using PlaywrightTests.Pages.Projects;

namespace PlaywrightTests
{
    [TestFixture]
    public class TestFixture
    {
        private IPage? page;
        private IBrowserContext? context;
        private LoginPage? loginPage;
        private DashboardPage? dashboardPage;
        private ProjectsPage? projectsPage;
        private readonly string[] TreeTypes = ["Coniferous Trees", "Deciduous Trees", "Fruit Trees", "Savanna Trees", "Tropical Trees", "Urban Trees"];
        private readonly string[] TreeTrunkCanopy = ["50", "40", "25", "60", "30", "40"];

        [SetUp]
        public async Task Setup()
        {           
            context = await GlobalSetup.Browser!.NewContextAsync();
            page = await context.NewPageAsync();
            loginPage = new(page);
            dashboardPage = new(page);
            projectsPage = new(page);
            await page!.GotoAsync(AppConfig.AppUrl!, new PageGotoOptions
            {
                Timeout = 50000 
            });            
            await loginPage!.Login(AppConfig.Email!, AppConfig.Password!);

        }

        [Test]
        public async Task VerifyProjectCases()
        {
           // Test 1: Verify project is built and added
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("WiFi");
            await projectsPage!.SelectVendor("Aruba");
            await projectsPage!.SelectModel("A574");
            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.VerifyProjectNameAfterOpening("MultiplePolygonPoints");
            await projectsPage!.ClickHardwareButton();

            //Test 2: drag and drop AP
            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.VerifyIconOnMap("1");

            //Test 3: drag and drop Idf
            await projectsPage!.ClickIdfTab();
            await projectsPage!.ClickAddAnIdfButton();
            await projectsPage!.EnterSwitchName("IDF1");
            await projectsPage!.ClickSwitchButton();
            await projectsPage!.EnterIdfName("Switch1");
            await projectsPage!.EnterNumberOfPorts("2");
            await projectsPage!.EnterTotalPower("100");
            await projectsPage!.ClickAddIdfButton();
            await projectsPage!.DragAndDrop(120, 190);
            await projectsPage!.VerifyIconOnMap("2");

            //Test 4: verify Idf ports

            await projectsPage!.ClickIdfRecordRow("IDF1");
            await projectsPage!.AssertUsedPortsValue("1");
            await projectsPage!.AssertFreePortsValue("1");
            await projectsPage!.ClickCancelButton();
            await projectsPage!.ClickIdfPlusIcon();

            //Test 5: Add wiring 
            await projectsPage!.EnterSwitchName("IDF2");
            await projectsPage!.ClickSwitchButton();
            await projectsPage!.EnterIdfName("Switch2");
            await projectsPage!.EnterNumberOfPorts("2");
            await projectsPage!.EnterTotalPower("100");
            await projectsPage!.ClickAddIdfButton();
            await projectsPage!.DragAndDrop(120, 250);            
            await Task.Delay(3000); // Wait for 3 seconds
            await projectsPage!.ClickEyeIcon();
            await Task.Delay(3000); // Wait for 3 seconds
            await projectsPage!.CLickWiringButton();
            await Task.Delay(3000); // Wait for 3 seconds
            await projectsPage!.ClickIdfRecordRow("IDF1");
            await Task.Delay(3000); // Wait for 3 seconds

            //Test 6: Verify layers visibility
            await projectsPage!.ClickEyeIcon();

            //Test 6.1: Verify hardware visibility
            await projectsPage!.ClickHardwareIconAsync();
            await projectsPage!.VerifyIconNotOnMap("1");
            await projectsPage!.VerifyIconNotOnMap("2");
            await projectsPage!.VerifyIconNotOnMap("3");

            await projectsPage!.ClickHardwareIconAsync();

            //Test 6.2: Verify labels visibility            
            await projectsPage!.ClickLabelsIconAsync();
            await projectsPage!.VerifyWifiIconLabelAPNotOnMapAsync(1, "1");

            //Test 6.3: Verify channel visibility
            await projectsPage!.ClickChannelsIconAsync();
            await projectsPage!.VerifyWifiIconChannelAsync(5, "38");

            //Test 6.4: Verify pins visibility
            await projectsPage!.ClickPinsIconAsync();
            await projectsPage!.VerifyPinsNotOnMap("place1");
            await projectsPage!.VerifyPinsNotOnMap("place2");
            await projectsPage!.VerifyPinsNotOnMap("place3");

            await projectsPage!.ClickIdfRecordRow("IDF1");

            //Test 7: Verify antennas
            await projectsPage!.ClickAPsTab();
            await projectsPage!.ClickFirstApRecordRow();
            await projectsPage!.ClickAntennasTab();
            await projectsPage!.ClickAntennaTypeDropDown();
            await projectsPage!.HoverOverAntennaTypeDropDownSecondOption();

            //Test8: verify panel displayed
            await projectsPage!.VerifyAntennaMenuPanelDisplayed();

            //Test8: verify gain value 
            await projectsPage!.VerifyGainValue("13");

            await projectsPage!.SelectBand("2.4GHz");

            //Test8: verify gain value after 2.4GHz
            await projectsPage!.VerifyGainValue("13");

            //Test9: Close Panel 
            await projectsPage!.CloseAntennaMenuPannel();
            await projectsPage!.VerifyAntennaMenuPanelNotDisplayed();

            //Test 10: Verify antenna panel displayed again after mouse over
            await projectsPage!.HoverOverAntennaTypeDropDownSecondOption();
            await projectsPage!.VerifyAntennaMenuPanelDisplayed();

            await projectsPage!.CloseAntennaMenuPannel();

            await page!.GotoAsync(AppConfig.AppUrl!, new PageGotoOptions
            {
                Timeout = 50000 
            });   
            
            //Test 11 : Delete Project
            await projectsPage!.MouseOverClickCreatedProject();
            await projectsPage!.ClickProjectMenu();
            await projectsPage!.ClickProjectDelete();
            await projectsPage!.ClickConfirmDelete();
            
            
        }
        [Test]
        public async Task VerifyWallsCases()
        {
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("WiFi");
            await projectsPage!.SelectVendor("Aruba");
            await projectsPage!.SelectModel("A574");
            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.VerifyProjectNameAfterOpening("MultiplePolygonPoints");

            //Test 1: verify wall types values
            await projectsPage!.ClickWallsButtonAsync();
            await projectsPage!.VerifyWallTypeValue(1, "0");
            await projectsPage!.VerifyWallTypeValue(2, "0");
            await projectsPage!.VerifyWallTypeValue(3, "0");
            await projectsPage!.VerifyWallTypeValue(4, "0");
            await projectsPage!.VerifyWallTypeValue(5, "0");
            await projectsPage!.VerifyWallTypeValue(6, "0");
            await projectsPage!.VerifyWallTypeValue(7, "0");

            //Test 2: verify brick thickness Db
            await projectsPage!.ClickWallTypeButtonAsync();
            await projectsPage!.SelectMeterialType("Brick");
            await projectsPage!.FillThicknessAsync("10");
            await projectsPage!.VerifyThicknessDb("6.47");
            await projectsPage!.FillHeightAsync("10");
            await projectsPage!.ClickSaveButtonWallsInfoAsync();
            await Task.Delay(3000); // Wait for 3 seconds

            //Test 3: verify added wall type value equal to zero
            await projectsPage!.VerifyWallTypeValue(1, "0");

            //Test 4: Draw square
            await projectsPage!.DrawSquareAsync(120, 120, 100);
            await Task.Delay(3000); // Wait for 3 seconds
            await projectsPage!.VerifyWallTypeValue(1, "5");

            //Test 4: verify thickness
            await projectsPage!.VerifyTenFtWallThicknessDisplayed();

            //Test 5 : Delete Wall
            await projectsPage!.ClickWallKebabMenu();
            await projectsPage!.ClickSelectAllButton();
            await projectsPage!.ClickDeleteWallButton();
            await Task.Delay(3000); 
            await projectsPage!.ClickDeleteWallsAlertButton();

            //Test 6 : verify created wall not displayed
            await projectsPage!.ClickWallsButtonAsync();
            await Task.Delay(3000); 
            await projectsPage!.VerifyCreatedWallNotDisplayed();
            
            // //Test 7 : Delete Project
            await projectsPage!.ClickAppIcon();
            await projectsPage!.MouseOverClickCreatedProject();
            await projectsPage!.ClickProjectMenu();
            await projectsPage!.ClickProjectDelete();
            await projectsPage!.ClickConfirmDelete();
            
            
        }
        [Test]
        public async Task VerifyObstructionCases()
        {
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("WiFi");
            await projectsPage!.SelectVendor("Aruba");
            await projectsPage!.SelectModel("A574");
            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.VerifyProjectNameAfterOpening("MultiplePolygonPoints");

            //Test 1: verify obstruction is added 
            await projectsPage!.ClickObstructionsButton();
            await projectsPage!.ClickAddObstructionButton();
            await projectsPage!.SelectObstructionMaterial("Brick");
            await projectsPage!.FillThicknessAsync("10");
            await projectsPage!.FillRoofThicknessInput("10");
            await projectsPage!.FillHeightAsync("10");
            await projectsPage!.ClickAddTypeButton();

            //Test 2: Obstruction value
            await projectsPage!.VerifyWallTypeValue(50, "0");

            //Test 2: Thickness value
            await projectsPage!.VerifyTenFtWallThicknessDisplayedObstruction();

            //Test 3: Draw square
            await projectsPage!.DrawSquareAsync(120, 120, 100);
            await projectsPage!.VerifyWallTypeValue(50, "1");

            //Test 4: Delete obstruction
            await projectsPage!.HoverAndClickCreateObstructionKebabBrickMenu();
            await projectsPage!.ClickSelectAllButton();
            await projectsPage!.HoverAndClickObstructionKebabMenuButton();
            await projectsPage!.ClickObstructionDeleteButton();
            await Task.Delay(3000); 
            await projectsPage!.ClickDeleteWallsAlertButton();

            //Test 5: verify created obstruction not displayed
            await projectsPage!.ClickObstructionsButton();
            await Task.Delay(3000); 
            await projectsPage!.VerifyCreatedObstructionNotDiplayed();

            // //Test 6 : Delete Project
            await projectsPage!.ClickAppIcon();
            await projectsPage!.MouseOverClickCreatedProject();
            await projectsPage!.ClickProjectMenu();
            await projectsPage!.ClickProjectDelete();
            await projectsPage!.ClickConfirmDelete();
            
            
        }
        [Test]
        public async Task VerifyMeasurementToolCases()
        {
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("WiFi");
            await projectsPage!.SelectVendor("Aruba");
            await projectsPage!.SelectModel("A574");
            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.VerifyProjectNameAfterOpening("MultiplePolygonPoints");

            await projectsPage!.ClickMeasure();

            //Test1: verify start measure text displayed
            await projectsPage!.VerifyMeasureTextOnCanvasAsync();
            await Task.Delay(3000); 

            //Test1: verify line of sight panel displayed
            await projectsPage!.DrawStraightLineAsync(0, -100, 0, 100);
            await projectsPage!.VerifyLineOfSightPanelDisplayedAsync();

            //Test3: Verify longitude and latitude on panel and canvas
           // await projectsPage!.VerifyLatitudeLongitudeMatchAsync();

            //Test4: Verify graph panel diplays
            await projectsPage!.VerifyMeasureGraphPanelIsDisplayed();

            //Test4: Verify graph panel not diplays
            await projectsPage!.CloseGraphPanel();
            await projectsPage!.VerifyMeasureGraphPanelIsNotDisplayed();

            //Test5: Verify line of site panel not displayed
            await projectsPage!.CloseLineOfSightPanel();
            await projectsPage!.VerifyLineOfSightPanelNotDisplayedAsync();

           
            // //Test 6 : Delete Project
            await projectsPage!.ClickAppIcon();
            await projectsPage!.MouseOverClickCreatedProject();
            await projectsPage!.ClickProjectMenu();
            await projectsPage!.ClickProjectDelete();
            await projectsPage!.ClickConfirmDelete();
            
            
        }
        [Test]
        public async Task VerifyDsmCases()
        {           
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("WiFi");
            await projectsPage!.SelectVendor("Aruba");
            await projectsPage!.SelectModel("A574");
            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.VerifyProjectNameAfterOpening("MultiplePolygonPoints");

            await projectsPage!.ClickDsm();

            //Test1: verify tree types and their trunk canopy values
            for(int i =0; i<6; i++)
            {
                await projectsPage!.SelectTreeType(TreeTypes[i]);
                await projectsPage!.VerifyTreeTrunkCanopyValue(TreeTrunkCanopy[i]);
            }               
           
            // //Test 6 : Delete Project
            await projectsPage!.ClickAppIcon();
            await projectsPage!.MouseOverClickCreatedProject();
            await projectsPage!.ClickProjectMenu();
            await projectsPage!.ClickProjectDelete();
            await projectsPage!.ClickConfirmDelete();
            
            
        }



        [TearDown]
        public async Task TearDown()
        {
            if (page != null)
            {
                await page.CloseAsync();
            }
            if (context != null)
            {
                await context.CloseAsync();
            }
        }
    }
}
