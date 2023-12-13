using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public class SessionTracker
    {
        public string Id { get; set; }
        public string FilterCount { get; set; }
        public string DocumentCount { get; set; }
        public string SearchCount { get; set; } 
        public string PhysicianCount { get; set; }
        public int TotalCount
        {
            get
            {
                var z = 0;
                try
                {
                    z = int.Parse(FilterCount) + int.Parse(DocumentCount) + int.Parse(SearchCount) + int.Parse(PhysicianCount);
                }
                catch { z = 0; }
                return z;
            }
        }


        public SessionTracker()
        {
            Id = Guid.NewGuid().ToString();
            FilterCount = "0";
            DocumentCount = "0";
            SearchCount = "0";
            PhysicianCount = "0";
        }
        
        public void IncrementFilterCount()
        {
            var x = int.Parse(FilterCount);
            x++;
            FilterCount = x.ToString();
        }
        public void IncrementDocumentCount()
        {
            var x = int.Parse(DocumentCount);
            x++;
            DocumentCount = x.ToString();
        }
        public void IncrementSearchCount()
        {
            var x = int.Parse(SearchCount);
            x++;
            SearchCount = x.ToString();
        }
        public void IncrementPhysicianCount()
        {
            var x = int.Parse(PhysicianCount);
            x++;
            PhysicianCount = x.ToString();
        }

        public void ResetCount()
        {
            FilterCount = "0";
            DocumentCount = "0";
            SearchCount = "0";
            PhysicianCount = "0";
        }
    }
}

