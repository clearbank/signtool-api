using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ClearBank.Signing.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHost(args)
                .Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.AddServerHeader = false;
                })
                .UseStartup<Startup>()
                .ConfigureLogging(logging => logging
                    .AddFilter("System", LogLevel.Error)
                    .AddFilter("Microsoft", LogLevel.Error))
                .Build();
    }
}
