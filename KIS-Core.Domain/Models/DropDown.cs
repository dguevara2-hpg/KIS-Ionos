using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KIS_Core.Domain.Models
{    
    public class DropDown
    {
        [DataMember(Name = "value")]
        [JsonProperty(PropertyName = "value")]
        
        public int id { get; set; }
        public string value { get; set; }
    }
}