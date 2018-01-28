using System;
using System.Threading.Tasks;
using DeepStreamNet;

namespace net
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new DeepStreamClient("localhost", 6020);

            if (await client.LoginAsync())
            {
                Console.WriteLine("Logged in.");

                var disp = await client.Events.SubscribeAsync("test", Console.WriteLine);
                Console.WriteLine("Subscribed to 'test'.");

                await Task.Delay(2000);

                client.Events.Publish("test", "Hello World");
                Console.WriteLine("Published 'test: Hello World'.");

                await Task.Delay(3000);

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();

                await disp.DisposeAsync();
            }

            client.Dispose();

            Console.WriteLine("Press any key to quit.");
            Console.Read();
        }
    }
}
