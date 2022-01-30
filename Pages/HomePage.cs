using System.Threading.Tasks;
using Microsoft.Playwright;

namespace FiestStore.Pages
{
    public class HomePage
    {
        private readonly IPage _page;

        private const string SEARCH_ITEMS_INPUT_CSS = "#search_query_top";

        public HomePage(IPage page)
        {
            _page = page;
        }

        public async Task SearchItem(string searchItemValue)
        {
            await _page.FillAsync(SEARCH_ITEMS_INPUT_CSS, searchItemValue);
            await _page.PressAsync(SEARCH_ITEMS_INPUT_CSS, "Enter");
        }
    }
}