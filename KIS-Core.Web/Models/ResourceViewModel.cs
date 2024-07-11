using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class ResourceViewModel
    {
        public KmLibrary Document { get; set; }

        public string BreadCrumControl { get; set; }
        public string BreadCrumPage { get; set; }

        public string Summary { get; set; }
        public string Overview { get; set; }
        public string Doc64bit { get; set; }
    }
}
