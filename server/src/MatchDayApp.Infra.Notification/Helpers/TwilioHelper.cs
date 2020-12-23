namespace MatchDayApp.Infra.Notification.Helpers
{
    public static class TwilioHelper
    {
        public static string FormatarNumeroNoPadraoDoTwilio(string number)
        {
            number = number
                .Trim()
                .Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "");

            return number;
        }
    }
}
