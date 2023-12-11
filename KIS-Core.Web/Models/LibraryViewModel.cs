using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class LibraryViewModel
    {
        public List<KmLibrary> Library { get; set; }
        public KmFilter Filter { get; set; }


        /// <summary>
        /// Dropdowns
        /// </summary>
        public List<string> Sorting { get; set; }
        public List<string> Specialties { get; set; }
        public List<string> Departments { get; set; }
        public List<string> ContentTypes { get; set; }
        public string Error { get; set; }


        public LibraryViewModel()
        {
            Library = new List<KmLibrary>();
            Filter = new KmFilter();
            Sorting = new List<string>();

            Specialties = new List<string>();
            Departments = new List<string>();
            ContentTypes = new List<string>();

            Error = string.Empty;
        }
    }
}