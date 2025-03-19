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
        private string? projectName ="MultiplePolygonPoints"; 


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
                Timeout = 120000 
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
            projectsPage!.CaptureGraphQLRequestHeatMap(page!);
            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.VerifyIconOnMap("1");
            projectsPage!.VerifyAPIResponse();

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
            await Task.Delay(5000);
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
            
            
        }
        [Test]
        public async Task VerifyWifiAPDeletion()
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

            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.VerifyIconOnMap("1");
            await projectsPage!.HoverOverChildAPRecordRow();
            await projectsPage!.ClickChildApKebabMenu();
            await projectsPage!.ClickDeleteKebabMenu();
            await projectsPage!.ClickConfirmDeleteAP();

            await projectsPage!.PressEscapeKey();
            await projectsPage!.VerifyIconNotOnMap("1");
            await projectsPage!.VerifyChildAPRecordRowNotExists();

            await projectsPage!.ClickApParentRecordRow("A574");
            await projectsPage!.DragAndDrop(120, 120);

            await projectsPage!.VerifyIconOnMap("1");
            await projectsPage!.VerifyChildAPRecordRowExists();

            
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
            await projectsPage!.VerifyWallTypeValue(1, "4");

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
            await projectsPage!.VerifyLatitudeLongitudeMatchAsync();

            //Test4: Verify graph panel diplays
            await projectsPage!.VerifyMeasureGraphPanelIsDisplayed();

            //Test4: Verify graph panel not diplays
            await projectsPage!.CloseGraphPanel();
            await projectsPage!.VerifyMeasureGraphPanelIsNotDisplayed();

            //Test5: Verify line of site panel not displayed
            await projectsPage!.CloseLineOfSightPanel();
            await projectsPage!.VerifyLineOfSightPanelNotDisplayedAsync();
            
            
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
            
            
        }
        [Test]
        public async Task VerifySummary()
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
            await projectsPage!.ClickHardwareButton();

            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            
            //drag and drop two aps
            await projectsPage!.DragAndDrop(120, 120);
            await Task.Delay(5000);
            await projectsPage!.DragAndDrop(120, 160);
            await Task.Delay(5000);

            //drag and drop one idf
            await projectsPage!.ClickIdfTab();
            await projectsPage!.ClickAddAnIdfButton();
            await projectsPage!.EnterSwitchName("IDF1");
            await projectsPage!.ClickSwitchButton();
            await projectsPage!.EnterIdfName("Switch1");
            await projectsPage!.EnterNumberOfPorts("2");
            await projectsPage!.EnterTotalPower("100");
            await projectsPage!.ClickAddIdfButton();
            await projectsPage!.DragAndDrop(120, 210);

            //Test1: Verify summary project and layout name
            await projectsPage!.ClickSummary();
            await projectsPage!.VerifyProjectAndLayoutNamesAsync();

            //Test2: Verify Aps no of records
           await projectsPage!.VerifyWifiRecordsNumberSummary(2);

            //Test3: Verify on materials tab (APs Models and Count)

            await projectsPage!.ClickMaterialsTab();            
            await projectsPage!.VerifySummaryMaterialsWifiModelAsync("A574");
            await projectsPage!.VerifySummaryMaterialsWifiCountAsync("2");

            //Test4: Verify on materials tab (IDF Models and Count)
            await projectsPage!.VerifySummaryMaterialsIDFModelAsync("2 Port Switch");
            await projectsPage!.VerifySummaryMaterialsIDFCountAsync("1");


        }
        [Test]
        public async Task VerifyWifiChangesOnly()
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
            await projectsPage!.ClickHardwareButton();

            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Indoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            
            //drag and drop ap
            await projectsPage!.DragAndDrop(120, 120);

            //Test1: verify after update Unit parent changes reflect in child

            await projectsPage!.ClickApParentKebabButtonAsync();
            await projectsPage!.ClickEditConfigButtonAsync();

            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Cambium");
            await projectsPage!.ClickUpdateApButtonAsync();

            await projectsPage!.ClickApRecordSidePanelCrossButtonAsync("e500");
            await projectsPage!.ClickChildAPRecordRow();

            await projectsPage!.VerifyApTypeAsync("Outdoor");
            await projectsPage!.VerifyVendorTypeAsync("Cambium");

            //Test2: verify after update config parent changes reflect in child
            await projectsPage!.ClickApRecordSidePanelCrossButtonAsync("e500");
            await projectsPage!.ClickApParentKebabButtonAsync();
            await projectsPage!.ClickEditConfigButtonAsync();

            await projectsPage!.ClickConfigTab();

            await projectsPage.SelectApConfigMountingAsync("Wall");
            await projectsPage.FillApConfigLossAsync("10");
            await projectsPage.FillApConfigHeightAsync("10");
            await projectsPage.FillApConfigAboveRoofAsync("10");
            await projectsPage.ClickApConfigAboveRoofCheckboxAsync();

            await projectsPage.SelectApConfigChannelWidthAsync("20");
            await projectsPage.FillApConfigTransmitPowerAsync("30");
            await projectsPage.SelectApConfigModulationSchemaAsync("BPSK");
            await projectsPage.SelectApConfigMimoAsync("2x2");
            await projectsPage.FillApConfigRequiredPOEPowerAsync("25");

            await projectsPage!.ClickUpdateApButtonAsync();
            await projectsPage!.ClickApRecordSidePanelCrossButtonAsync("e500");

            await Task.Delay(3000);

            await projectsPage!.ClickChildAPRecordRow();
            await projectsPage!.ClickConfigTab();

            await projectsPage.VerifyApConfigMountingAsync("Wall");
            await projectsPage.VerifyApConfigLossAsync("10");
            await projectsPage.VerifyApConfigHeightAsync("10");
            await projectsPage.VerifyApConfigAboveRoofCheckedAsync(true);
            await projectsPage.VerifyApConfigAboveRoofAsync("10");
            await projectsPage.VerifyApConfigChannelWidthAsync("20");
            await projectsPage.VerifyApConfigTransmitPowerAsync("30");
            await projectsPage.VerifyApConfigModulationSchemaAsync("BPSK");
            await projectsPage.VerifyApConfigMimoAsync("2x2");
            await projectsPage.VerifyApConfigRequiredPOEPowerAsync("25");


        }
        [Test]
        public async Task VerifyWifiOverrideChanges()
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
            await projectsPage!.ClickHardwareButton();

            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Indoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            
            //drag and drop ap
            await projectsPage!.DragAndDrop(120, 120);

            //changes in child
            await projectsPage!.ClickChildAPRecordRow();
            await projectsPage!.ClickConfigTab();
            await projectsPage.FillApConfigRequiredPOEPowerAsync("25");
            await projectsPage!.ClickApConfigWifiGhzCheckboxAsync();
            await projectsPage!.ClickApAntennasTabAsync();            
            await projectsPage!.FillApAntennasDownTiltAsync("20");            
            await projectsPage!.FillApAntennasAzimuthAsync("20");    
            await projectsPage!.ClickUpdateApButtonAsync();
            await projectsPage!.ClickApRecordSidePanelCrossButtonAP303Async("AP-303");       

            //changes in parent
            await projectsPage!.ClickApParentKebabButtonAsync();
            await projectsPage!.ClickEditConfigButtonAsync();
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Cambium"); 
            await projectsPage!.ClickApAntennasTabAsync();  
            await projectsPage!.FillApAntennasDownTiltAsync("0");            
            await projectsPage!.FillApAntennasAzimuthAsync("0");
            await projectsPage!.ClickOverrideRadioButton();
            await projectsPage!.ClickUpdateApButtonAsync();          
            await projectsPage!.ClickApRecordSidePanelCrossButtonAsync("e500");

            //verify overriden settings in child
            await projectsPage!.ClickChildAPRecordRow();
            await projectsPage!.ClickApAntennasTabAsync();            
            await projectsPage!.VerifyApAntennasDownTiltAsync("0");          
            await projectsPage!.VerifyApAntennasAzimuthAsync("0");          

        }
        [Test]
        public async Task VerifyCellularChangesOnly()
        {
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("Cellular");
            await projectsPage!.SelectVendor("Airspan");
            await projectsPage!.SelectModel("AirSpeed 1030");

            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.ClickHardwareButton();

            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectTechnology("Cellular");
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApCellularVendor("Airspan");

            await projectsPage!.ClickApCarriersTab();
            await projectsPage!.ClickApAddCarrierButton();
            await projectsPage!.FillApCarrierName("1");

            await projectsPage!.ClickApSectorsTab();
            await projectsPage!.ClickApAddSectorButton();
            await projectsPage!.FillApSectorName("1");
            await projectsPage!.SelectApCarriersDropdown("1");

            await projectsPage!.ClickAddApButton();
                      
            //drag and drop ap
            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.ClickChildApRecordRowCellular();
            await projectsPage!.ClickApCarriersTab();

            await projectsPage!.FillApCarrierLoss("10");
            await projectsPage!.ClickUpdateApButtonAsync();
            await projectsPage!.ClickCancelApButton();

            await projectsPage!.ClickApParentRecordRow("AirHarmony 4000");
            await projectsPage!.ClickApParentKebabButtonAsync();
            await projectsPage!.ClickEditConfigButtonAsync();
            await projectsPage!.ClickApCarriersTab();

            await projectsPage!.VerifyNotApCarrierLoss("10");

            await projectsPage!.ClickApSectorsTab();

            await projectsPage!.FillApSectorDownlit("10");
            await projectsPage!.FillApSectorAzimuth("10");

            await projectsPage!.ClickUpdateApButtonAsync();
            await projectsPage!.ClickCancelApButton();

            await projectsPage!.ClickChildApRecordRowCellular();
            await projectsPage!.ClickApCarriersTab();

            await projectsPage!.VerifyApCarrierLoss("10");
            await projectsPage!.ClickApSectorsTab();

            await projectsPage!.VerifyApSectorDownlit("10");
            await projectsPage!.VerifyApSectorAzimuth("10");
          

        }
        [Test]
        public async Task VerifyCellularSettingsOverride()
        {
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\MultiplePolygonPoints.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("Cellular");
            await projectsPage!.SelectVendor("Airspan");
            await projectsPage!.SelectModel("AirSpeed 1030");

            await projectsPage!.ClickNextButton();
            await projectsPage!.VerifyProjectBuilding();
            await projectsPage!.ClickCreatedProject("project-card-MultiplePolygonPoints.kmz");
            await projectsPage!.ClickHardwareButton();

            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectTechnology("Cellular");
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApCellularVendor("Airspan");

            await projectsPage!.ClickApCarriersTab();
            await projectsPage!.ClickApAddCarrierButton();
            await projectsPage!.FillApCarrierName("1");

            await projectsPage!.ClickApSectorsTab();
            await projectsPage!.ClickApAddSectorButton();
            await projectsPage!.FillApSectorName("1");
            await projectsPage!.SelectApCarriersDropdown("1");

            await projectsPage!.ClickAddApButton();
                      
            //drag and drop ap
            await projectsPage!.DragAndDrop(120, 120);

           //child cellular changes
            await projectsPage!.ClickChildApRecordRowCellular();
            await projectsPage!.ClickApCarriersTab();
            await projectsPage!.FillApCarrierLoss("10");
            await projectsPage!.ClickApSectorsTab();
            await projectsPage!.FillApSectorDownlit("10");
            await projectsPage!.FillApSectorAzimuth("10");
            await projectsPage!.ClickUpdateApButtonAsync();
            await projectsPage!.ClickCancelApButton();

           //parent cellular changes
            await projectsPage!.ClickApParentRecordRow("AirHarmony 4000");
            await projectsPage!.ClickApParentKebabButtonAsync();
            await projectsPage!.ClickEditConfigButtonAsync();
            await projectsPage!.ClickApCarriersTab();
            await projectsPage!.ClickApAddCarrierButton();
            await projectsPage!.FillApSecondCarrierName("2");
            await projectsPage!.ClickApSectorsTab();
            await projectsPage!.ClickApAddSectorButton();
            await projectsPage!.FillApSecondSectorName("2");
            await projectsPage!.SelectApSecondCarriersDropdown("2");
            await projectsPage!.ClickOverrideRadioButton();
            await projectsPage!.ClickUpdateApButtonAsync();
            await projectsPage!.ClickCancelApButton();

            // Verifications
            await projectsPage!.ClickChildApRecordRowCellular();
            await projectsPage!.ClickApCarriersTab();

            await projectsPage!.VerifyApCarrierLoss("0");
            await projectsPage.VerifyApCarrierNameVisibleAsync();
            await projectsPage.VerifyApSecondCarrierNameVisibleAsync();

            await projectsPage!.ClickApSectorsTab();

            await projectsPage.VerifyApSectorNameVisibleAsync();
            await projectsPage.VerifyApSecondSectorNameVisibleAsync();

            await projectsPage!.VerifyApSectorDownlit("0");
            await projectsPage!.VerifyApSectorAzimuth("0");
          

        }
        [Test]
        public async Task VerifyAnnotationsCases()
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

            await projectsPage!.ClickAnnotationsButton();
            await projectsPage!.ClickUploadOverlyImage();
            await projectsPage!.UploadOverlyImage("\\TestFiles\\sample.png");

            await projectsPage!.VerifyOverlyImage();

            await projectsPage!.ClickDeleteOverlayImageIcon();
            await projectsPage!.ClickDeleteOverlayButton();
            await projectsPage!.ClickAnnotationsTab();

            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.FillAnnotationsTextarea("Sample Comment");
            await projectsPage!.ClickAnnotationsAddCommentButton();

            await Task.Delay(3000);

            await projectsPage!.UploadOverlyImage("\\TestFiles\\sample.png");

            await Task.Delay(3000);

            await projectsPage!.ClickAnnotationsAddCommentButton();

            await projectsPage!.VerifyAnnotationsTextareaUploadedImgIsVisible();
            await projectsPage!.VerifyAnnotationsTextareaAddedCommentIsVisible("Sample Comment");
            await projectsPage!.ClickDeleteOverlayImageIcon();
            await projectsPage!.ClickDeleteOverlayButton();

            await Task.Delay(3000);

            await projectsPage!.VerifyAnnotationsTextareaUploadedImgIsNotVisible();
            await projectsPage!.VerifyAnnotationsTextareaAddedCommentIsNotVisible("Sample Comment");            
            
        }
        [Test]
        public async Task VerifySurveyCases()
        {
            projectName= "Gallo_bounds";
            await dashboardPage!.ClickImportProjectArchieve();
            await projectsPage!.UploadProjectFile("\\TestFiles\\Gallo_bounds.kmz");
            await projectsPage!.ClickNextButton();
            await projectsPage!.SelectTechnology("WiFi");
            await projectsPage!.SelectVendor("Aruba");
            await projectsPage!.SelectModel("A574");
            await projectsPage!.ClickNextButton();
            await projectsPage!.ClickCreatedProject("project-card-Gallo_bounds.kmz");

            await projectsPage!.ClickSurveyDataButton();
            await projectsPage!.ClickImportSiteSurveyDataButton();
            await projectsPage!.UploadSurveyFile("\\TestFiles\\gallo1.csv");
            await projectsPage!.ClickNextButton();

            await projectsPage!.SelectNetwork("Wi-Fi");
            await projectsPage!.SelectBandInSurvey("WiFi 5GHz");
            await projectsPage!.ClickImportButton();

            await projectsPage!.ClickSurveyDataDropDown();
            await projectsPage!.ClickSiteSurveyDataPlusIcon();

            await projectsPage!.UploadSurveyFile("\\TestFiles\\gallo2.csv");
            await projectsPage!.ClickNextButton();

            await projectsPage!.SelectNetwork("Wi-Fi");
            await projectsPage!.SelectBandInSurvey("WiFi 2.4GHz");
            await projectsPage!.ClickImportButton();

            await projectsPage!.SelectSurveyData("gallo1.csv");
            await projectsPage!.SelectSurveyData("gallo2.csv");

            await projectsPage!.ClickAway(120, 120);
            
            await projectsPage!.ClickWifiOnNavbar();
            await projectsPage!.ClickWifi5GHzRadioButton();

            await projectsPage!.VerifyIndependentExternalMapResponsOnPCI();
            await page!.WaitForTimeoutAsync(5000);
            await projectsPage!.VerifyIndependentExternalMap2Dot4ResponsOnPCI();

            await projectsPage!.ClickAway(120, 120);

            await projectsPage!.ClickSurveyDataDropDown();
            await projectsPage!.SelectSurveyFileTODeleteByIndex("1");
            await projectsPage!.SelectSurveyFileTODeleteByIndex("1");

            await projectsPage!.VerifyDeleteIconNotVisible(1);            
           
        }

        [TearDown]
        public async Task TearDown()
        {
            if (page != null)
            {
                await page!.GotoAsync(AppConfig.AppUrl!, new PageGotoOptions
                {
                    Timeout = 50000 
                });

                // await projectsPage!.MouseOverClickCreatedProject(projectName!);
                // await projectsPage!.ClickProjectMenu(projectName!);
                // await projectsPage!.ClickProjectDelete();
                // await projectsPage!.ClickConfirmDelete();

                await projectsPage!.HoverOverAndDeleteProjects(projectName!);

                await page.CloseAsync();
            }
            if (context != null)
            {
                await context.CloseAsync();
            }
        }
    }
}
