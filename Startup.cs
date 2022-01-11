using System;
using FiestStore.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace FiestStore
{
    public class Startup
    {
        private IServiceProvider ServiceProvider { get; }

        public Startup(IPage page)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IPage>();
            serviceCollection.AddSingleton<HomePage>();
            serviceCollection.AddSingleton<ItemPage>();
            serviceCollection.AddSingleton<BasePage>();                                                                 

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}