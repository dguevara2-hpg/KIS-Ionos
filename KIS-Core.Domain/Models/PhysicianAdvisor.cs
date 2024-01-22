using KIS_Core.Domain.Utilities;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public class PhysicianAdvisor
    {
        public int Id { get; set; } = 0;
        public string PhysicianLastName { get; set; } = string.Empty;
        public string PhysicianFirstName { get; set; } = string.Empty;
        public List<string> Specialty { get; set; }
        public List<string> Subspecialty { get; set; }
        public List<string> MedicalSchool { get; set; }
        public List<string> Residency { get; set; }
        public List<string> Fellowships { get; set; }
        public List<string> BoardCertifications { get; set; }
        public List<string> Credentials { get; set; }
        public List<string> HospitalAffiliations { get; set; }
        public string Biography { get; set; } = string.Empty;
        public List<string> FacilityType { get; set; }
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
        public List<string> IDN { get; set; }                                               
        public string Modified { get; set; } = string.Empty;        
        public string DisplayName { get { return PhysicianFirstName + " " + PhysicianLastName; } }
        public string Error { get; set; } = string.Empty;

        public PhysicianAdvisor()
        {
            Specialty = new List<string>();
            Subspecialty = new List<string>();
            MedicalSchool = new List<string>();
            Residency = new List<string>();
            Fellowships = new List<string>();
            BoardCertifications = new List<string>();
            Credentials = new List<string>();
            HospitalAffiliations = new List<string>();
            FacilityType = new List<string>();
        }
        public PhysicianAdvisor(PhysicianAdvisorString advisor)
        {
            Id = advisor.Id;
            PrimaryEmail = advisor.PrimaryEmail;
            Credentials = Utils.DelimitedToList('|',advisor.Credentials);
            Specialty = Utils.DelimitedToList('|', advisor.Specialty);
            Subspecialty = Utils.DelimitedToList('|', advisor.Subspecialty);
            //HealthSystem = Utils.DelimitedToList('|', advisor.HealthSystem);
            HospitalAffiliations = Utils.DelimitedToList('|', advisor.HospitalAffiliations);
            FacilityType = Utils.DelimitedToList('|', advisor.FacilityType);
            //Education = Utils.DelimitedToList('|', advisor.Education);
            Residency = Utils.DelimitedToList('|', advisor.Residency);
            Fellowships = Utils.DelimitedToList('|', advisor.Fellowships);
            BoardCertifications = Utils.DelimitedToList('|', advisor.BoardCertifications);
            Biography = advisor.Biography.Trim();
        }
    }
}
