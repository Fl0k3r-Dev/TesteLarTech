namespace TesteLarTech.Core.Notifications
{
    internal class NotificationService : INotificationService
    {
        private readonly List<DomainNotifications> _notifications = new();
        
        public IEnumerable<DomainNotifications> GetNotifications()
        {
            return _notifications.ToArray();
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Notify(string key, string message)
        {
            _notifications.Add(new DomainNotifications(key, message));
        }
        public void Clear()
        {
            _notifications.Clear();
        }
    }
}
