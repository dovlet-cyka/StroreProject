using System.Threading.Tasks;
using Microsoft.Playwright;

namespace FiestStore.Pages
{
    public class BasePage 
    {
        private const string WOMEN_BUTTON_CSS = "a[title='Women']";
        
        private readonly IPage _page;
        
        public BasePage(IPage page)
        {
            _page = page;
        }

        public async Task ClickWomenPage()
        {
            await _page.ClickAsync(WOMEN_BUTTON_CSS);
        }
    }
}