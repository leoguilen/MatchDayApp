namespace MatchDayApp.Domain.Configuration
{
    public class SmtpSettings
    {
        public string SmtpAddress { get; set; }
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
