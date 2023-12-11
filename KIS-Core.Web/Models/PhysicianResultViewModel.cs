using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class PhysicianResultViewModel
    {
        public List<PhysicianAdvisor> Result { get; set; } = new List<PhysicianAdvisor>();
        public List<PillTab> PillTabs { get; set; } = new List<PillTab>();
        public List<BannerImage> BannerImages { get; set; } = new List<BannerImage>();

        public string Error { get; set; } = string.Empty;
    }
}

