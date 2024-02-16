namespace KIS_Core.Web.Models
{
    public class EnvironmentConfig
    {
        public const string Position = "Position";
        public string CurrentSetting { get; set; } = string.Empty;        
        public string Version { get; set; } = string.Empty;
    }
}
