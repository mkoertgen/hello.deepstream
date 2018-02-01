using System;
using System.Threading.Tasks;
using DeepStreamNet;
using Newtonsoft.Json.Linq;

namespace ConsoleSample
{
    internal class Program
    {
        public static async Task Main()
        {
            var url = GetEnv("DEEPSTREAM_URL", "localhost:6020").Split(':');
            var host = url[0];
            var port = int.Parse(url[1]);

            Console.WriteLine($"Connecting to DeepStream Server '{host}:{port}'...");
            using (var client = new DeepStreamClient(host, port))
            {
                var user = GetEnv("DEEPSTREAM_USER", "userA");
                var pwd = GetEnv("DEEPSTREAM_PWD", "rCOSZxJrgze2AZdVQh12c6ErDMOG0M+Rx5Yu7S5d91c=GS4SbTQYmoaGwjm2shEobg==");
                var loggedIn = await client.LoginAsync(user, pwd);
                if (!loggedIn) throw new InvalidOperationException($"Could not login with user '{user}'");

                Console.WriteLine("Logged in.");

                var eventName = GetEnv("DEEPSTREAM_EVENT", "test");
                // https://github.com/dotnet/roslyn/issues/114
                //using (var disp = await client.Events.SubscribeAsync(eventName, Console.WriteLine))
                var disp = await client.Events.SubscribeAsync(eventName, Console.WriteLine);
                try
                {
                    Console.WriteLine($"Subscribed to '{eventName}'.");
                    await Task.Delay(1000);

                    const string data = "Hello from DotNet";
                    Console.WriteLine($"Publishing '{eventName}: '{data}'...");
                    client.Events.Publish(eventName, data);

                    var json = new JObject(new JProperty("a", 7), new JProperty("b", 8));
                    var result = await client.Rpcs.MakeRequest<JObject, float>("/nodejs/multiply-numbers", json);
                    Console.WriteLine($"Result: {result}");

                    while (true) await Task.Delay(1000);
                }
                finally
                {
                    await disp.DisposeAsync();
                }
            }
        }

        private static string GetEnv(string name, string defaultValue)
        {
            var value = Environment.GetEnvironmentVariable(name);
            return value ?? defaultValue;
        }
    }
}
