using System.Threading.Tasks;
using FiestStore.Config;
using FiestStore.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace FiestStore
{
    public class BaseTest : PageTest 
    {
        private const string WEBSITE_URL = "http:/automationpractice.com/index.php/";
        private const string JSON_FILE_NAME_WEBSITE_CONFIG = "appsettings.json";
        private const string SECTION_JSON_FILE_WEBSITE_CONFIG = "WebsiteConfig";

        protected ItemPage ItemPage;
        protected HomePage HomePage;

        private WebsiteConfig _websiteConfig;
        private IConfiguration _configuration;

        [SetUp]
        public async Task InitializeAllPages()
        {
            TestServer testServer = new(new WebHostBuilder().UseStartup<Startup>().ConfigureTestServices(collection =>
            {
                collection.AddSingleton(Page);
            }));
            
            
            HomePage = testServer.Services.GetService<HomePage>();
            ItemPage = testServer.Services.GetService<ItemPage>();
            
            await Page.GotoAsync(WEBSITE_URL);

            // _websiteConfig = _configuration.GetSection(SECTION_JSON_FILE_WEBSITE_CONFIG).Get<WebsiteConfig>();
            //.UseConfiguration(new ConfigurationBuilder().SetBasePath(CurrentDomain.BaseDirectory)
            // .AddJsonFile(JSON_FILE_NAME_WEBSITE_CONFIG).Build())
        }

        [TearDown]
        public async Task End()
        {
            await Page.CloseAsync();
        }
    }
}