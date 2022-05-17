using DemoImp.Entity;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace DemoImp.Repo
{
    public class MessagesDb
    {
        public async Task PushMessages(message message,string endpoint,string masterkey)
        {

            using (var client = new CosmosClient(endpoint, masterkey))
            {
                var container = client.GetContainer("messages", "messages");
                dynamic doc = new
                {
                    id = Guid.NewGuid(),
                    value = message.value,
                 //   name = "kanif",
                    flag = message.flag

                };
                await container.CreateItemAsync(doc, new PartitionKey(message.flag));
                Console.WriteLine($"Create document id {doc.id}");
            }
        }
    }
}
