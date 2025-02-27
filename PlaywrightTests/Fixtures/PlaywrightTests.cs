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
        public async Task VerifyFileUpload()
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
            await projectsPage!.ClickHardwareButton();

            //drag and drop AP
            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.VerifyIconOnMap("1");

            //drag and drop Idf
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

            //verify Idf ports

            await projectsPage!.ClickIdfRecordRow("IDF1");
            await projectsPage!.AssertUsedPortsValue("1");
            await projectsPage!.AssertFreePortsValue("1");
            await projectsPage!.ClickCancelButton();

            await projectsPage!.ClickIdfPlusIcon();

            //drag and drop another Idf
            await projectsPage!.EnterSwitchName("IDF2");
            await projectsPage!.ClickSwitchButton();
            await projectsPage!.EnterIdfName("Switch2");
            await projectsPage!.EnterNumberOfPorts("2");
            await projectsPage!.EnterTotalPower("100");
            await projectsPage!.ClickAddIdfButton();
            await projectsPage!.DragAndDrop(120, 250);

            await projectsPage!.ClickEyeIcon();
            await projectsPage!.CLickWiringBytton();
            await projectsPage!.ClickIdfRecordRow("IDF1");

            //project delete implementation

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
