using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Server
{
    class Program
    {
        private const string Uri = "http://localhost:8080/news";

        static void Main(string[] args)
        {
            // Start News service
            using (ServiceHost host = new ServiceHost(typeof(NewsService), new Uri(Uri)))
            {
                host.AddServiceEndpoint(typeof(INewsService), new WSDualHttpBinding(), "");

                ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                behavior.HttpGetEnabled = true;
                host.Description.Behaviors.Add(behavior);
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                host.Open();
                Log.ServiceStartInfo(Uri);

                // Start Rss Service
                RssService.Start();

                // Waiting for interrupt
                Console.ReadLine();
            }
        }
    }
}
