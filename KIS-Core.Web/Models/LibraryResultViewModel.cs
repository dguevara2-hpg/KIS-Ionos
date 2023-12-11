using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class LibraryResultViewModel
    {
        public List<KmLibrary> Result { get; set; } = new List<KmLibrary>();
        public List<PillTab> PillTabs { get; set; } = new List<PillTab>();
        public List<BannerImage> BannerImages { get; set; } = new List<BannerImage>();
        public string Error { get; set; } = string.Empty;
    }
}

