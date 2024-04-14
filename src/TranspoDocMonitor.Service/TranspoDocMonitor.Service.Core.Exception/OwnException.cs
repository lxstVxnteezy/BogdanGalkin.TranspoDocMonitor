

namespace TranspoDocMonitor.Service.Core.Exception
{
    public class OwnException : System.Exception
    {
        public int Code { get; }

        public string UserMessage { get; }

        public string? InnerMessage { get; }

        public OwnException(
            int code,
            string userMessage,
            string? innerMessage = null)
        {
            Code = code;
            InnerMessage = innerMessage;
            UserMessage = userMessage;
        }
    }
}
