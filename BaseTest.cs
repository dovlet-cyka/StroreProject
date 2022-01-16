using System.Threading.Tasks;
using FiestStore.Config;
using FiestStore.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using NUnit.Framework;
using static System.AppDomain;

namespace FiestStore
{
    public class BaseTest
    {
        protected ItemPage ItemPage;
        protected HomePage HomePage;

        private const string WEBSITE_URL = "http:/automationpractice.com/index.php/";
        private const string JSON_FILE_NAME_WEBSITE_CONFIG = "appsettings.json";
        private const string SECTION_JSON_FILE_WEBSITE_CONFIG = "WebsiteConfig";

        private WebsiteConfig _websiteConfig;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private IConfiguration _configuration;
        
        [SetUp]
        public void InitializeAllPages()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(CurrentDomain.BaseDirectory)
                .AddJsonFile(JSON_FILE_NAME_WEBSITE_CONFIG).Build();
            _websiteConfig = _configuration.GetSection(SECTION_JSON_FILE_WEBSITE_CONFIG).Get<WebsiteConfig>();
            
            InitializePageObject().GetAwaiter().GetResult();
            TestServer testServer = new TestServer(new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(_page);
                })
                .UseStartup<Startup>());

             _page.GotoAsync(WEBSITE_URL).GetAwaiter().GetResult();
            HomePage = testServer.Services.GetService<HomePage>();
            ItemPage = testServer.Services.GetService<ItemPage>();
        }

        [TearDown]
        public void End()
        {
            _playwright.Dispose();
            _browser.CloseAsync();
            _page.CloseAsync();
        }

        private async Task InitializePageObject()
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright[_websiteConfig.BrowserType].LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = _websiteConfig.Headless,
                ExecutablePath = _websiteConfig.WebsiteLocationWindows
            });

            _page = await _browser.NewPageAsync();
        }
    }
}