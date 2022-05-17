using System;
using System.Threading.Tasks;
using DemoImp.Entity;
using DemoImp.Repo;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System.Linq;

namespace DemoImp
{
    public class Function1
    {
        private  readonly IConfiguration _configuration;
        //
        private readonly IFeatureManagerSnapshot _featureManagerSnapshot;
        private readonly IConfigurationRefresher _configurationRefresher;

        public Function1(IConfiguration configuration,IFeatureManagerSnapshot featureManagerSnapshot, IConfigurationRefresherProvider refresherProvider)
        {
            _featureManagerSnapshot = featureManagerSnapshot;
            _configurationRefresher = refresherProvider.Refreshers.First();
            _configuration = configuration;
        }
        //

        [FunctionName("Function1")]
        public async Task RunAsync([ServiceBusTrigger("mytopic", "s1", Connection = "ConfigurationString")] string mySbMsg, ILogger log)
        {
            var doc = new message();
            doc.value = mySbMsg;
            string []flag = {"green", "blue", "yellow", "red","black"};
            var random = new Random();
            int num = random.Next(4);
            doc.flag = flag[num];

            //app configuration
            string endpoints = _configuration["endpoint"];
            string endpoint = "https://azureserv.documents.azure.com:443/";
            string masterkeys = _configuration["masterKey"];
            string masterkey = "gqkxQFxrvasnM8knOcJPynxSv7c3VCSik6KzhIE15rh2DZNowWmodNUiWJwVXiyYyHIokRq8VTcb56fHj8BJrg==";
            var messages = new MessagesDb();
            await messages.PushMessages(doc,endpoint,masterkey);

            // feature flag
            string m = "oono";
          //  string m = await _featureManagerSnapshot.IsEnabledAsync("FeatureA")
          //? "The Feature Flag 'FeatureA' is turned ON"
          //: "The Feature Flag 'FeatureA' is turned OFF";
          //  //
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg} and feature flag {m}");
        }
    }
}
