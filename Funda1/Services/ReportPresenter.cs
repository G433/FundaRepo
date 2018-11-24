using System;
using System.Collections.Generic;
using Funda1.Interfaces;
using Funda1.Models;

namespace Funda1.Services
{
    public class ReportPresenter : IReportPresenter
    {
        /// <summary>
        /// Present the top makelaar by asset count table 
        /// </summary>
        /// <param name="makelaarToAssets"></param>
        public void Present(Dictionary<int, Makelaar> makelaarToAssets)
        {
            Console.WriteLine("\n\nAssetCount".PadRight(17) + "MakelaarID".PadRight(15) + "MakelaarNaam".PadRight(17));
            Console.WriteLine($"-------------------------------------------------------");
            foreach (var item in makelaarToAssets)
            {
                Console.WriteLine($"{item.Value.AssetCount}".PadRight(15) + $"{item.Key}".PadRight(15) + $"{item.Value.Name}".PadRight(15));
            }
        }
    }
}
