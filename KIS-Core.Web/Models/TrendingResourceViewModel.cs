using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class TrendingResourceViewModel
    {
        public List<KmLibrary> Featured { get; set; }
        public List<KmLibrary> Product { get; set; }
        public List<KmLibrary> Evidence { get; set; }
        public List<KmLibrary> Physician { get; set; }

        public TrendingResourceViewModel()
        {
            Featured = new List<KmLibrary>();
            Product = new List<KmLibrary>();
            Evidence = new List<KmLibrary>();
            Physician = new List<KmLibrary>();
        }

    }
}
