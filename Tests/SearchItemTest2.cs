using System.Threading.Tasks;
using NUnit.Framework;

namespace FiestStore.Tests 
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class SearchItemTest2 : BaseTest
    {
        private const string SEARCH_ITEM_TEXT = "T-shirt";
        
        [Test]
        public async Task SearchItem()
        {
            await HomePage.SearchItem(SEARCH_ITEM_TEXT);
            await ItemPage.ValidateCorrectPage();
            Assert.IsTrue(false);
        }
    }
}