namespace TranspoDocMonitor.Service.Core.Exception
{
    public class ServiceOwnErrorAttribute : Attribute
    {
        public string Message { get; }

        public ServiceOwnErrorAttribute(string message)
        {
            Message = message;
        }
    }
}
