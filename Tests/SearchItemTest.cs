using System.Threading.Tasks;
using NUnit.Framework;

namespace FiestStore.Tests 
{
    [TestFixture]
    public class SearchItemTest : BaseTest 
    {
        [Test]
        public async Task SearchItem()
        {
            await HomePage.SearchItem(SearchItemVariables.SearchItem);
            await ItemPage.ValidateCorrectPage();
        }
    }
}