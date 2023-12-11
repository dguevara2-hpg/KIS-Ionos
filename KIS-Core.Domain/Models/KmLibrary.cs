using System;
using System.Collections.Generic;

namespace KIS_Core.Domain.Models
{
    public class KmLibrary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Provisioning { get; set; }
        public List<string> ContractNumber { get; set; }
        public DateTime? Modified { get; set; }
        public string DocumentTitle { get; set; }
        public string ItemType { get; set; }
        public List<string> InsightCategory { get; set; }
        public List<string> Category { get; set; }
        public string ModifiedBy { get; set; }
        public List<string> Department { get; set; }
        public List<string> Specialty { get; set; } 
        public string Path { get; set; }

        // used in the past, but not part of latest export
        public string Sequence { get; set; }
        public string DocumentPreviewText { get; set; }
        public string HTInitiative { get; set; }


        public KmLibrary()
        {
            Id = 0;
            Name = "";
            Link = "";
            Provisioning = "";
            ContractNumber = new List<string>(); 
            Modified = null;
            DocumentTitle = "";
            ItemType = "";
            InsightCategory = new List<string>();
            Category = new List<string>();
            ModifiedBy = "";
            Department = new List<string>();
            Specialty = new List<string>();
            Path = "";
            Sequence = "";
            DocumentPreviewText = "";
            HTInitiative = "";
        }

    }

}