using System;
using FiestStore.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace FiestStore
{
    public class Startup
    {
        // public IConfiguration Configuration { get; }
        //
        // public Startup(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HomePage>();
            services.AddSingleton<ItemPage>();
            services.AddSingleton<BasePage>();
        }

        public void Configure(IPage page)
        {
            Console.WriteLine("DSA");
        }
    }
}