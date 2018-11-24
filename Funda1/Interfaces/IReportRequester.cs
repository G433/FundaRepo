using System.Threading.Tasks;
using Funda1.Models;

namespace Funda1.Interfaces
{
    public interface IReportRequester
    {
        Task<Report> RequestAsync();
    }
}