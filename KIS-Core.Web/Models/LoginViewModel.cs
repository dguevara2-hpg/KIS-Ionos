using System.Collections.Generic;
using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class LoginViewModel
    {
        public User user { get; set; }        
        public string username { get; set; }
        public string password { get; set; }
    }
}