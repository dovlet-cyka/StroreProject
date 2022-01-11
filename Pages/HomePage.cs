using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace FiestStore.Pages
{
    public class HomePage : IMyPage
    {
        private readonly IPage _page;

        private const string SEARCH_ITEMS_INPUT_CSS = "#search_query_top";

        public HomePage(IPage page)
        {
            _page = page;
        }

        public async Task SearchItem(string searchText)
        {
            await _page.FillAsync(SEARCH_ITEMS_INPUT_CSS, searchText);
            await _page.PressAsync(SEARCH_ITEMS_INPUT_CSS, "Enter");
        }

        public void PrintHello()
        {
            Console.WriteLine("Hello");
        }
    }
}