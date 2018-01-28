using System;
using System.Threading.Tasks;
using DeepStreamNet;

namespace ConsoleSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = GetEnv("DEEPSTREAM_URL", "localhost:6020").Split(':');
            var dsHost = url[0];
            var dsPort = int.Parse(url[1]);

            Console.WriteLine($"Connecting to DeepStream Server '{dsHost}:{dsPort}'...");
            var client = new DeepStreamClient(dsHost, dsPort);

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

        static string GetEnv(string name, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(name);
            return value ?? defaultValue;
        }
    }
}
