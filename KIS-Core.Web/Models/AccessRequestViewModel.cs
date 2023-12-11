using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class AccessRequestViewModel
    {
        // filters

        //data
        public List<User> CurrentUsers { get; set; }

        public AccessRequestViewModel()
        {
            CurrentUsers = new List<User>();
        }
    }
}