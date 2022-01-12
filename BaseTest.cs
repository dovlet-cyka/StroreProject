using System;
using System.Threading.Tasks;
using FiestStore.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using NUnit.Framework;

namespace FiestStore
{
    public class BaseTest
    {
        private IConfiguration _config;
        
        protected ItemPage ItemPage { get; set; }
        protected HomePage HomePage { get; set; } 
        
        public string URL { get; set; }
        public string websiteUrl { get; set; }

        [SetUp]
        public void Start()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();


            IConfigurationSection section = config.GetSection("Chromium");
            BaseTest weatherClientConfig = section.Get<BaseTest>();
            Console.WriteLine(weatherClientConfig.URL);
            Console.WriteLine(URL);
            Console.WriteLine("Finish");
            
            
            Startup startup = new Startup(GetPageObject().Result, new ServiceCollection());
            ItemPage = startup.ServiceProvider.GetService<ItemPage>();
            HomePage = startup.ServiceProvider.GetService<HomePage>();
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
    }
}