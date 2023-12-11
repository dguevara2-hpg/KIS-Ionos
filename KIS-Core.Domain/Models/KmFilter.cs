using System.Collections.Generic;

namespace KIS_Core.Domain.Models
{
    public class KmFilter
    {
        public string SelSearch { get; set; }
        public string SelSort { get; set; }
        public string SelSpecialty { get; set; }
        public string SelService { get; set; }
        public string SelContentType { get; set; }

        public List<PillTab> PillTabs { get; set; }

        //public bool isValid { get { if ((SelSpecialty == null || SelSpecialty == "")  && (SelService == null || SelService == "") && (SelContentType == null || SelContentType == "")) { return false; } else { return true; } } }
        public bool isValid { get { if (SelSpecialty == null && SelService == null && SelContentType == null ) { return false; } else { return true; } } }


        public KmFilter()
        {
            PillTabs = new List<PillTab>();
        }

    }
}