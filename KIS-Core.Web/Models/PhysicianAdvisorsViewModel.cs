using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class PhysicianAdvisorDetailViewModel
    {
        public PhysicianAdvisor physicianAdvisor { get; set; }
        public PhysicianEngagementScores physicianEngagementScores { get; set; }
        public string Error { get; set; } = string.Empty;        

        // tobe used for dropdowns
        public List<DropDown> ddlCredentials { get; set; }
        public List<DropDown> ddlSpecialties { get; set; }        
        public List<DropDown> ddlSubSpecialties { get; set; }
        public List<DropDown> ddlHealthSystems { get; set; }        
        public List<DropDown> ddlFacilityTypes { get; set; }
        public List<DropDown> ddlBoardCertifications { get; set; }
        public List<DropDown> ddlSpecialInterests { get; set; }

        // History
        public List<PhysicianAdvisor> RequestActivity { get; set; }

        // Disable forms
        public bool DisableContactForm { get; set; }
        public bool DisableProfileForm { get; set; }


        public PhysicianAdvisorDetailViewModel()
        {
            physicianAdvisor = new PhysicianAdvisor();
            physicianEngagementScores = new PhysicianEngagementScores();


            RequestActivity = new List<PhysicianAdvisor>();

            DisableContactForm = false;
            DisableProfileForm = false;
    }
    }
}
