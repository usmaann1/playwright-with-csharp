using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

[SetUpFixture]
public class GlobalSetup
{
    public static IPlaywright? Playwright { get; private set; }
    public static IBrowser? Browser { get; private set; }
    public static IBrowserContext? Context { get; private set; }
    public static IPage? Page { get; private set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true, 
            Args = new[] { "--disable-gpu", "--window-size=1920,1080" }
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
            IgnoreHTTPSErrors = true 

        });

        Context.SetDefaultTimeout(20000); 
        // await Context.AddInitScriptAsync("window.matchMedia('(prefers-reduced-motion: reduce)').matches = true;");
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        Console.WriteLine("Global Teardown: Closing Browser");
        await Browser!.CloseAsync();
        Playwright!.Dispose();
    }

 
}
