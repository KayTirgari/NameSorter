using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NameSorter.Shared.Abstractions;
using NameSorter.Shared.Implementations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NameSorter.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var filename = args != null && args.Any() ? args[0] : "";
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Please provide the input file name");
                return;
            }

            using var host = CreateHostBuilder(args).Build();
            using var serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            // Retrieve the required service
            var processingService = provider
                .GetRequiredService<IProcessingService>();
            await processingService.ProcessAsync(filename);

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IInputFileService, InputFileService>()
                            .AddTransient<ISortingService, SortingService>()
                            .AddTransient<IProcessingService, ProcessingService>()
                            .AddTransient<INameExtractionService, NameExtractionService>());
            return host;
        }   
    }
}
