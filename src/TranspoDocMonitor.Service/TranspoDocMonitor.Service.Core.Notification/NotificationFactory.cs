public class NotificationFactory
{
    private readonly IEnumerable<INotificationStrategy> _notificationStrategies;

    public NotificationFactory(IEnumerable<INotificationStrategy> notificationStrategies)
    {
        _notificationStrategies = notificationStrategies;
    }

    public INotificationStrategy GetStrategy(NotificationType notificationType)
    {
        var strategy = _notificationStrategies.SingleOrDefault(s => s.CanHandle(notificationType));
        if (strategy == null)
        {
            throw new NotSupportedException($"No strategy found for handling {notificationType} notifications.");
        }
        return strategy;
    }
}   