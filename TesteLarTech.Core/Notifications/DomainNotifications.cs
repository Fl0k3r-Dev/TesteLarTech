namespace TesteLarTech.Core.Notifications
{
    public class DomainNotifications
    {
        public DomainNotifications(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; }
        public string Message { get; }
    }
}
