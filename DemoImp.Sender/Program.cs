using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DemoImp.Sender
{
    class Program
    {
        public static string connectionString = "Endpoint=sb://tpkmsgs.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ul16TbGOXVgq71MvG5BcykMdIA8Qt94iisvsnAu4hGE=";

        public static string topicName = "mytopic";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Send message to console!");
            var text = Console.ReadLine();
            await SendTextString(text);

        }
        static async Task SendTextString(string text)
        {
            Console.WriteLine("SendTextStringAsMessageAsync", ConsoleColor.Cyan);

            //create client
            var client = new TopicClient(connectionString, topicName);

            Console.Write("Sending....", ConsoleColor.Green);

            var message = new Message(Encoding.UTF8.GetBytes(text));

            await client.SendAsync(message);

            Console.WriteLine("Done!", ConsoleColor.Green);
               
            //alway close the client
            await client.CloseAsync();

        }
    }
}
