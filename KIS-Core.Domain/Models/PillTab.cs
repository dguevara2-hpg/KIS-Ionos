namespace KIS_Core.Domain.Models
{
    public class PillTab
    {
        public PillTab(string types, string value)
        {
            this.types = types;
            this.value = value;
        }

        public string types { get; set; }
        public string value { get; set; }
    }
    
}
