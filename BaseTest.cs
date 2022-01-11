using System;
using System.Threading.Tasks;
using FiestStore.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;

namespace FiestStore
{
    public class BaseTest
    {
        private IConfiguration _config;
        
        protected ItemPage ItemPage { get; set; }
        protected HomePage HomePage { get; set; }

        [SetUp]
        public void Start()
        {
            _config = InitConfiguration();
            Console.WriteLine(_config["websiteUrl"]);
            // Startup startup = new Startup(GetPageObject().Result);            
            // ItemPage = startup.ServiceProvider.GetService<ItemPage>();
            // HomePage = startup.ServiceProvider.GetService<HomePage>();
        }

        private async Task<IPage> GetPageObject()
        {
            IPlaywright playwright = await Playwright.CreateAsync();

            IBrowser browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false, ExecutablePath = _config["chromiumSettings:URL"]
            });

            IBrowserContext context = await browser.NewContextAsync();
            IPage page = await context.NewPageAsync();

            await page.GotoAsync(_config["websiteUrl"]);
            return page;
        }
        
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("C:/Users/user/Desktop/FiestStore/appsettings.json")
                .Build();
            return config;
        }
        
        
        private const string LINK_TO_WEBSITE = "http:\\automationpractice.com\\index.php";
        private const string CHROMIUM_LOACTION = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
        // private const string CHROMIUM_LOACTION = "/usr/bin/chromium-browser";
    }
}