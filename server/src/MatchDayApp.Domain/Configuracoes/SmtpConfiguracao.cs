namespace MatchDayApp.Domain.Configuracoes
{
    public class SmtpConfiguracao
    {
        public string SmtpAddress { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 465;
        public bool UseSsl { get; set; } = true;
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
