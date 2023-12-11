using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KIS_Core.Domain.Models
{    
    public class DropDown
    {
        [DataMember(Name = "value")]
        [JsonProperty(PropertyName = "value")]
        public string value { get; set; }
    }
}