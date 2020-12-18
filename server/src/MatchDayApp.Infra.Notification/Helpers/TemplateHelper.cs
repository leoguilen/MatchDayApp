using System.IO;

namespace MatchDayApp.Infra.Notification.Helpers
{
    public static class TemplateHelper
    {
        public static string ObterWelcomeTemplateHtml()
        {
            return File.ReadAllText("_Templates/welcome_template.html");
        }
    }
}
