using KIS_Core.Domain.Utilities;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public class PhysicianAdvisorString
    {
        public int Id { get; set; } = 0;
        public string PhysicianLastName { get; set; } = string.Empty;
        public string PhysicianFirstName { get; set; } = string.Empty;
        public string Specialty { get; set; }
        public string Subspecialty { get; set; }
        public string MedicalSchool { get; set; }
        public string Residency { get; set; }
        public string Fellowships { get; set; }
        public string BoardCertifications { get; set; }
        public string Credentials { get; set; }
        public string HospitalAffiliations { get; set; }
        public string Biography { get; set; } = string.Empty;
        public string FacilityType { get; set; }
        public string NPI { get; set; } = string.Empty;
        public string HourlyRate { get; set; } = string.Empty;
        public string TravelRate { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string VendorNumber { get; set; } = string.Empty;
        public string InternalTags { get; set; } = string.Empty;
        public string SpeakerRating { get; set; } = string.Empty;
        public string Cv { get; set; } = string.Empty;
        public string Headshot { get; set; } = string.Empty;
        public string PrimaryEmail { get; set; } = string.Empty;
        public string SecondaryEmail { get; set; } = string.Empty;
        public string IDN { get; set; }                                               
        public string Modified { get; set; } = string.Empty;        
        public string DisplayName { get { return PhysicianFirstName + " " + PhysicianLastName; } }
        public string Error { get; set; } = string.Empty;

        public PhysicianAdvisorString()
        {
            
        }
        public PhysicianAdvisorString(PhysicianAdvisor advisor)
        {            
            Id = advisor.Id;                     
            Credentials = Utils.ListToDelimited('|', Utils.SortList(advisor.Credentials));
            Specialty = Utils.ListToDelimited('|', Utils.SortList(advisor.Specialty));
            Subspecialty = Utils.ListToDelimited('|', Utils.SortList(advisor.Subspecialty));
            IDN = Utils.ListToDelimited('|', advisor.IDN);
            HospitalAffiliations = Utils.ListToDelimited('|', Utils.SortList(advisor.HospitalAffiliations));
            FacilityType = Utils.ListToDelimited('|', Utils.SortList(advisor.FacilityType));
            MedicalSchool = Utils.ListToDelimited('|', advisor.MedicalSchool);
            Residency = Utils.ListToDelimited('|', Utils.SortList(advisor.Residency));
            Fellowships = Utils.ListToDelimited('|', Utils.SortList(advisor.Fellowships));
            BoardCertifications = Utils.ListToDelimited('|', Utils.SortList(advisor.BoardCertifications));
            Biography = advisor.Biography.Trim();
        }        
    }
}
