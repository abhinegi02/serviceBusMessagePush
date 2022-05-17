using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.FeatureManagement;

[assembly: FunctionsStartup(typeof(DemoImp.Startup))]

namespace DemoImp
{
    class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            //  string cs = "Endpoint=https://cosmoconnection.azconfig.io;Id=ECDZ-l0-s0:l+7G1Y0D5/XP9KZBP7UE;Secret=iBwqujuJIW/GJfFoOqM4aEjGlVxfBD4lyJvJ/8PccLg=";
                string cs = "Endpoint=https://cosmosconnection.azconfig.io;Id=D0dR-lh-s0:qZymWRGjWDOMRxt9sLVw;Secret=8pNcBtGcR7BVFVYeghtHfdt6fZz3KIVI+SK0MHFCZs8=";
            builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            {
                options.Connect(cs).ConfigureKeyVault(kv =>
            {
                kv.SetCredential(new DefaultAzureCredential());
            }).UseFeatureFlags();
            });
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAzureAppConfiguration();
            builder.Services.AddFeatureManagement();
        }
    }
}