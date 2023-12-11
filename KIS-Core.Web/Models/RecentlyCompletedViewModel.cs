using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class RecentlyCompletedViewModel
    {
        public List<KmLibrary> CompletedResc { get; set; }

        public RecentlyCompletedViewModel()
        {
            CompletedResc = new List<KmLibrary>();
        }
    }
}
