using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public class User
    {
        public string guid { get; set; } = "";
        public string username { get; set; } = "";
        public string huddleId { get; set; } = "";
        public int roleID { get; set; } = 0;
        public string role { get; set; } = "";
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public string facilityIDN { get; set; } = "";
        public int facilityID { get; set; } = 0;
        public string jobTitle { get; set; } = "";
        public string emailAddress { get; set; } = "";
        public string password { get; set; } = "";
        public string resetPasswordKey { get; set; } = "";
        public string resetPasswordTstamp { get; set; } = "";

        public DateTime requestedDate { get; set; } = new DateTime();
        public Boolean isApproved { get; set; } = false;
        public string approvedBy { get; set; } = "";
        public string approvedDate { get; set; } = "";
        public int statusID { get; set; } = 0;
        public string statusName { get; set; } = "";


        public string notes { get; set; } = "";
        public Boolean sendEmail { get; set; } = false;
        public string EmailType { get; set; } = "";
    }
}

