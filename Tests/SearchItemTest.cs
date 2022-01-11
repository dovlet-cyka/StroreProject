using System.Threading.Tasks;
using NUnit.Framework;

namespace FiestStore.Tests 
{
    [TestFixture]
    public class SearchItemTest : BaseTest 
    {
        private const string SEARCH_ITEM = "T-shirt";

        [Test]
        public async Task SearchItem()
        {
            await Startup.homePage.SearchItem(SEARCH_ITEM);
            await Startup.itemPage.ValidateCorrectPage();
        }
    }
}