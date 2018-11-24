using System.Collections.Generic;
using System.Threading.Tasks;
using Funda1.Models;

namespace Funda1.Interfaces
{
    public interface IReportsAggregator
    {
        Task<List<Report>> AggregateReportsAsync(string baseUrl);
    }
}