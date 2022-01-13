using System;
using System.Threading.Tasks;
using FiestStore.Config;
using FiestStore.jsonSchemaInjection.variables;
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
        protected SearchItemVariables SearchItemVariables { get; private set; }

        private ChromiumConfig _chromiumConfig;

        [SetUp]
        public void Start()
        {
            _chromiumConfig = InitializeJsonFileProvider("appsettings.json")
                .GetSection("ChromiumConfig").Get<ChromiumConfig>();
            SearchItemVariables = InitializeJsonFileProvider("SearchItem.json", "..\\..\\..\\jsonSchemaInjection\\jsons").
                GetSection("SearchItem").Get<SearchItemVariables>();

            Startup startup = new Startup(new ServiceCollection(),  GetPageObject().Result);
            ItemPage = startup.ServiceProvider.GetService<ItemPage>();
            HomePage = startup.ServiceProvider.GetService<HomePage>();
        }

        private IConfigurationRoot InitializeJsonFileProvider(string jsonFile, string link="")
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory + link)
                .AddJsonFile(jsonFile).Build();
        }

        private async Task<IPage> GetPageObject()
        {
            IPlaywright playwright = await Playwright.CreateAsync();

            IBrowser browser = await playwright[_chromiumConfig.BrowserType].LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = _chromiumConfig.Headless, ExecutablePath = _chromiumConfig.ChromiumPathWindows
            });
            
            IPage page = await browser.NewPageAsync();

            await page.GotoAsync(_chromiumConfig.WebsiteUrl);
            return page;
        }
    }
}