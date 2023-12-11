using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public class PhysicianEngagementScores
    {
        public int ID { get; set; }
        public string PhysicianLastName { get; set; }
        public string PhysicianFirstName { get; set; }
        public int NumberOfEngagements { get; set; }
        public int NumberOfResponses { get; set; }
        public string PercentOpportunityScore { get; set; }
        public int OpportunityScore { get; set; }
        public int PulseAttendee { get; set; }
        public int TheSourceContributor { get; set; }
        public int Huddle { get; set; }
        public int HTUAttendee { get; set; }
        public int LiveEducation_Innovation_ClinicalRequest { get; set; }
        public decimal WeightedScore { get; set; }
        public decimal TotalScore { get; set; }
        public string TotalScorePercent { get; set; }
    }
}
