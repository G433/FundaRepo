using System;
using Funda1.Interfaces;
using Funda1.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Funda1
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUrl = @"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=";

            ServiceProvider serviceProvider = ConfigureServices();
            var reportsManager = serviceProvider.GetService<IReportsManager>();

            try
            {
                while (true)
                {
                    Console.WriteLine("\nPlease choose from the following options:");
                    Console.WriteLine("1 - assets in Amsterdam");
                    Console.WriteLine("2 - assets in Amsterdam with tuin");
                    Console.WriteLine("q - exit program\n");

                    var choice = Console.ReadKey();

                    switch (choice.KeyChar)
                    {
                        case '1':
                            reportsManager.MakelaarReportsAsync(baseUrl + "/amsterdam/").GetAwaiter().GetResult();
                            break;
                        case '2':
                            reportsManager.MakelaarReportsAsync(baseUrl + "/amsterdam/tuin/").GetAwaiter().GetResult();
                            break;
                        case 'q':
                        case 'Q':
                            Console.WriteLine("Exit program...");
                            return;
                        default:
                            Console.WriteLine("Unrecognized option, please try again..");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit the program...");
                Console.ReadKey();
                throw;
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IReportsAggregator, ReportsAggregator>()
                .AddSingleton<IReportPresenter, ReportPresenter>()
                .AddSingleton<IReportsManager, ReportsManager>()
                .BuildServiceProvider();
        }
    }
}
