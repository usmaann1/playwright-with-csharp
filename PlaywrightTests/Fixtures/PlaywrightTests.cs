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
            await projectsPage!.ClickAddAnApButton();
            await projectsPage!.SelectApType("Outdoor");
            await projectsPage!.SelectApVendor("Aruba");
            await projectsPage!.ClickAddApButton();
            await projectsPage!.DragAndDrop(120, 120);
            await projectsPage!.VerifyIconOnMap("1");

            await projectsPage!.ClickIdfTab();
            await projectsPage!.ClickAddAnIdfButton();
            await projectsPage!.EnterSwitchName("Test");
            await projectsPage!.ClickSwitchButton();
            await projectsPage!.EnterIdfName("sample");
            await projectsPage!.EnterNumberOfPorts("12");
            await projectsPage!.EnterTotalPower("12");
            await projectsPage!.ClickAddIdfButton();
            await projectsPage!.DragAndDrop(180, 180);
            await projectsPage!.VerifyIconOnMap("2");

            //project delete implementation pending 

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
