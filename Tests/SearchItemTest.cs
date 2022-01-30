using System.Threading.Tasks;
using NUnit.Framework;


namespace FiestStore.Tests 
{
    [TestFixture, Parallelizable(ParallelScope.Children)]
    public class SearchItemTest : BaseTest
    {
        private const string SEARCH_ITEM_TEXT = "T-shirt";

        [Test]
        public async Task SearchItem()
        {
            await HomePage.SearchItem(SEARCH_ITEM_TEXT);
            await ItemPage.ValidateCorrectPage();
        }
        
        [Test]
        public async Task SearchItem2()
        {
            await HomePage.SearchItem(SEARCH_ITEM_TEXT);
            await ItemPage.ValidateCorrectPage();
        }
    }
}