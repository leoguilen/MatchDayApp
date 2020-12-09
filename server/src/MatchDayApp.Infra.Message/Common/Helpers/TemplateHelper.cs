using System.IO;

namespace MatchDayApp.Infra.Message.Common.Helpers
{
    public static class TemplateHelper
    {
        public static string GetWelcomeTemplateToString()
        {
            return File.ReadAllText("_Templates/welcome_template.html");
        }
    }
}
