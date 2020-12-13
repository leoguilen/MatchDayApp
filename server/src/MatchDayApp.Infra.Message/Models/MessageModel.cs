using MatchDayApp.Infra.Message.Common.Helpers;

namespace MatchDayApp.Infra.Message.Models
{
    public class MessageModel
    {
        public string From { get; set; }
        public string To
        {
            get { return TwilioHelper.FormatPhoneNumber(To); }

            set { To = value; }
        }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
