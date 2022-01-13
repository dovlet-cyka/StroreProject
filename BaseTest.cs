using System;
using System.Threading.Tasks;
using FiestStore.Config;
using FiestStore.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using NUnit.Framework;

namespace FiestStore
{
    public class BaseTest
    {
        protected ItemPage ItemPage;
        protected HomePage HomePage;

        private const string WEBSITE_URL = "http:/automationpractice.com/index.php/";
        
        private WebsiteConfig _websiteConfig;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public void Start()
        {
            _websiteConfig = InitializeJsonFileProvider("appsettings.json")
                .GetSection("WebsiteConfig").Get<WebsiteConfig>();

            InitializePageObject().Wait();
            Startup startup = new Startup(new ServiceCollection(), _page);
            ItemPage = startup.ServiceProvider.GetService<ItemPage>();
            HomePage = startup.ServiceProvider.GetService<HomePage>();
            
            _page.GotoAsync(WEBSITE_URL);
        }

        [TearDown]
        public void End()
        {
            _playwright.Dispose();
            _browser.CloseAsync();
            _page.CloseAsync();
        }
        
        private IConfigurationRoot InitializeJsonFileProvider(string jsonFile)
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(jsonFile).Build();
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