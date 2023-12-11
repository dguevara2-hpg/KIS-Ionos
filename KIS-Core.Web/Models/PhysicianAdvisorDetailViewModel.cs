using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class PhysicianAdvisorsViewModel
    {
        public KmFilter Filter { get; set; }


        /// <summary>
        /// Dropdowns
        /// </summary>
        public List<string> Sorting { get; set; }
        public List<string> Specialties { get; set; }
        public List<string> IDNS { get; set; }
        
        public string Error { get; set; }



        public PhysicianAdvisorsViewModel()
        {
            Filter = new KmFilter();
            Sorting = new List<string>();

            Specialties = new List<string>();
            IDNS = new List<string>();

            Error = string.Empty;
        }
    }
}
