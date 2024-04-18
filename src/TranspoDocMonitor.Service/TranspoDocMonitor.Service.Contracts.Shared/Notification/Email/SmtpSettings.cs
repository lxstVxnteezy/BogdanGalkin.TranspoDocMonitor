namespace TranspoDocMonitor.Service.Contracts.Shared.Notification.Email;

public class SmtpSettings
{
    public string Host = null!;
    public int Port;
    public string Username = null!;
    public string Password = null!;
    public string SenderName = null!;
    public string SenderEmail = null!;

}