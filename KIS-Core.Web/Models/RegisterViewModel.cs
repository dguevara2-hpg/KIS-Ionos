using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class RegisterViewModel
    {
        public string guid { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Facilities> facilityIDNlist{ get; set; }
        public string jobTitle { get; set; }
        public string emailAddress { get; set; }        
    }
}