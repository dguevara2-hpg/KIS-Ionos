using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<UserRoles> UserRoles { get; set; }
        public List<RequestStatus> RequestStatus { get; set; }
        public List<Facilities> Facilities { get; set; }
        public string InternalNote { get; set; }

        public List<RequestActivity> RequestActivity { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public UserViewModel()
        {
            User = new User();
            UserRoles = new List<UserRoles>();
            RequestStatus = new List<RequestStatus>();
            Facilities = new List<Facilities>();
            InternalNote = string.Empty;
            RequestActivity = new List<RequestActivity>();            
        }
    }
}