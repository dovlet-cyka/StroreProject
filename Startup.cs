using System;
using FiestStore.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace MyStoreAutomation
{
    public class Startup
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;
        
        public ItemPage itemPage { get; }
        public HomePage homePage { get; }

        public Startup(IPage page)
        {
            ConfigureServices(page);
            homePage = _serviceProvider.GetService<HomePage>();
            itemPage = _serviceProvider.GetService<ItemPage>();
        }
        
        private void ConfigureServices(IPage page)
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton(new HomePage(page));
            _serviceCollection.AddSingleton(new ItemPage(page));
            _serviceCollection.AddSingleton(new BasePage(page));

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
    }
}