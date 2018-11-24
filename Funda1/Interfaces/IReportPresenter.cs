using System.Collections.Generic;
using Funda1.Models;

namespace Funda1.Interfaces
{
    public interface IReportPresenter
    {
        void Present(Dictionary<int, Makelaar> makelaarToAssets);
    }
}