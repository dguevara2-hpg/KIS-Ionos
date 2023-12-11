using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class HomeViewModel
    {

        public TrendingResourceViewModel Trending { get; set; }        

        public RecentlyCompletedViewModel Completed { get; set; }

        public List<InProcess> InProcess { get; set; }
        public List<MostViewedDocuments> MostViewed { get; set; }


        public HomeViewModel()
        {
            Trending = new TrendingResourceViewModel();            
            Completed = new RecentlyCompletedViewModel();
            InProcess = new List<InProcess>();
            MostViewed = new List<MostViewedDocuments>();
        }

    }
}
