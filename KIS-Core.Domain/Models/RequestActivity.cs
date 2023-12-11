using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public class RequestActivity
    {
        public int ActivityID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int RequestStatusID { get; set; }
        public string RequestStatusName { get; set; }
        public string EmailType { get; set; }
        public string UserGUID { get; set; }
        public string UserName { get; set; }
        public string InternalNote { get; set; }
        public DateTime ActivityDate { get; set; }

    }
}
