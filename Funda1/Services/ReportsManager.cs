using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funda1.Interfaces;
using Funda1.Models;

namespace Funda1.Services
{
    public class ReportsManager : IReportsManager
    {
        private readonly IReportsAggregator _reportAggregator;
        private readonly IReportPresenter _reportPresenter;

        public ReportsManager(IReportsAggregator reportAggregator ,IReportPresenter reportPresenter)
        {
            _reportAggregator = reportAggregator;
            _reportPresenter = reportPresenter;
        }

        /// <summary>
        /// Make reports presenting the top makelaars sorted by the amount of their offered properties
        /// </summary>
        /// <param name="url">url of the funda api</param>
        public async Task MakelaarReportsAsync(string url)
        {
            var reports = await _reportAggregator.AggregateReportsAsync(url).ConfigureAwait(false);
            _reportPresenter.Present(GetTopMakelaars(reports));
        }

        private Dictionary<int, Makelaar> GetTopMakelaars(List<Report> reports, int topCount = 10)
        {
            var makelaarObjectCount = new Dictionary<int, Makelaar>();

            foreach (var report in reports)
            {
                foreach (var asset in report.Objects)
                {
                    if (makelaarObjectCount.ContainsKey(asset.MakelaarId))
                    {
                        makelaarObjectCount[asset.MakelaarId].AssetCount++;
                    }
                    else
                    {
                        makelaarObjectCount[asset.MakelaarId] = new Makelaar
                        {
                            Name = asset.MakelaarNaam,
                            AssetCount = 1
                        };
                    }
                }
            }
            
            return new Dictionary<int, Makelaar>(makelaarObjectCount.OrderByDescending(x => x.Value.AssetCount).Take(topCount)); 
        }
    }
}
