namespace TesteLarTech.Core.Notifications
{
    public interface INotificationService
    {
        IEnumerable<DomainNotifications> GetNotifications();
        bool HasNotifications();
        void Notify(string key, string message);
        void Clear();
    }
}
