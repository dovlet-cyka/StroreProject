using System.Threading.Tasks;
using Microsoft.Playwright;

namespace FiestStore.Pages
{
    public class ItemPage 
    {
        private readonly IPage _page;

        private const string CURRENT_PAGE_CSS = ".navigation_page";

        public ItemPage(IPage page)
        {
            _page = page;
        }

        public async Task<string> ValidateCorrectPage()
        {
            return await _page.InnerTextAsync(CURRENT_PAGE_CSS);
        }
    }
}