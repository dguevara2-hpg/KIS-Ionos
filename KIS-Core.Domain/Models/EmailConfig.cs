namespace KIS_Core.Domain.Models
{
    public class EmailConfig
    {
        public string From { get; set; } = string.Empty;
        public string From_Name { get; set; } = string.Empty;        
        public string SmtpServer { get; set; } =string.Empty;
        public int Port { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
        public string RequestAccessPath { get; set; } = string.Empty;
        public string RequestAccessSubject { get; set; } = string.Empty;
        public bool RequestAccessHtml { get; set; } = false;

        public string RequestAccessNotificationPath { get; set; } = string.Empty;
        public string RequestAccessNotificationSubject { get; set; } = string.Empty;
        public bool RequestAccessNotificationHtml { get; set; } = false;

        public string TeamNotificationEmail { get; set; } = string.Empty;        
        public string TeamNotificationEmail_Name { get; set; } = string.Empty;
        public string EmailTemplates { get; set; } = string.Empty;
        public string PhysicianUpdateRequestPath { get; set; } = string.Empty;
        public string PhysicianUpdateRequestEmail { get; set;} = string.Empty;
        public string PhysicianUpdateRequestSubject { get; set; } = string.Empty;
    }
}
