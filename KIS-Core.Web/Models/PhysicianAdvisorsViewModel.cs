using KIS_Core.Domain.Models;

namespace KIS_Core.Web.Models
{
    public class PhysicianAdvisorDetailViewModel
    {
        public PhysicianAdvisor physicianAdvisor { get; set; }
        public PhysicianEngagementScores physicianEngagementScores { get; set; }
        public string Error { get; set; } = string.Empty;

        public PhysicianAdvisorDetailViewModel()
        {
            physicianAdvisor = new PhysicianAdvisor();
            physicianEngagementScores = new PhysicianEngagementScores();
        }
    }
}
