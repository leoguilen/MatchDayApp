namespace MatchDayApp.Infra.Message.Common.Helpers
{
    public static class TwilioHelper
    {
        public static string FormatPhoneNumber(string number)
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
