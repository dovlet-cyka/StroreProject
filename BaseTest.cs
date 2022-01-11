using System.Threading.Tasks;
using Microsoft.Playwright;
using MyStoreAutomation;
using NUnit.Framework;

namespace FiestStore
{
    public class BaseTest
    {
        private const string LINK_TO_WEBSITE = "http:\\automationpractice.com\\index.php";
        // private const string CHROMIUM_LOACTION = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
        private const string CHROMIUM_LOACTION = "/usr/bin/chromium-browser";

        protected Startup Startup;
        

        [SetUp]
        public void SetUp()
        {
            Startup = new Startup(GetPageObject().Result);
        }

        private static async Task<IPage> GetPageObject()
        {
            IPlaywright playwright = await Playwright.CreateAsync();

            IBrowser browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false/*, Channel = "chrome"*/, ExecutablePath = CHROMIUM_LOACTION, SlowMo = 50
            });

            IBrowserContext context = await browser.NewContextAsync();
            IPage page = await context.NewPageAsync();

            await page.GotoAsync(LINK_TO_WEBSITE);
            return page;
        }
    }
}