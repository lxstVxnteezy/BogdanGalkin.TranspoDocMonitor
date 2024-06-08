public interface INotificationStrategy
{
    Task SendNotificationAsync(string recipient, string message, CancellationToken cancellationToken);
    bool CanHandle(NotificationType notificationType);
}