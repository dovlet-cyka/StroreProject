﻿using System;
using FiestStore.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace FiestStore
{
    public class Startup
    {
        public IServiceProvider ServiceProvider { get; }

        public Startup(IPage page, IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(page);
            serviceCollection.AddSingleton<HomePage>();
            serviceCollection.AddSingleton<ItemPage>();
            serviceCollection.AddSingleton<BasePage>();                                                                 

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}