using System.Collections.Generic;

namespace Funda1.Models
{
    public class Report
    {
        public int AccountStatus { get; set; }
        public bool EmailNotConfirmed { get; set; }
        public bool ValidationFailed { get; set; }
        public object ValidationReport { get; set; }
        public int Website { get; set; }
        public Metadata Metadata { get; set; }
        public IList<Asset> Objects { get; set; }
        public Paging Paging { get; set; }
        public int TotaalAantalObjecten { get; set; }
    }
}