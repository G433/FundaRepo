using System.Threading.Tasks;

namespace Funda1.Interfaces
{
    public interface IReportsManager
    {
        Task MakelaarReportsAsync(string url);
    }
}