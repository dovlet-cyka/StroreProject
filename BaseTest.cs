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
        protected ItemPage ItemPage { get; private set; }
        protected HomePage HomePage { get; private set; }

        private ChromiumConfig _chromiumConfig;

        [SetUp]
        public void Start()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            _chromiumConfig = config.GetSection("Chromium").Get<ChromiumConfig>();

            Startup startup = new Startup(GetPageObject().Result, new ServiceCollection());
            ItemPage = startup.ServiceProvider.GetService<ItemPage>();
            HomePage = startup.ServiceProvider.GetService<HomePage>();
        }

        private async Task<IPage> GetPageObject()
        {
            IPlaywright playwright = await Playwright.CreateAsync();

            IBrowser browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = _chromiumConfig.Headless, ExecutablePath = _chromiumConfig.ChromiumPathLinux
            });

            IBrowserContext context = await browser.NewContextAsync();
            IPage page = await context.NewPageAsync();

            await page.GotoAsync(_chromiumConfig.WebsiteUrl);
            return page;
        }
    }
}