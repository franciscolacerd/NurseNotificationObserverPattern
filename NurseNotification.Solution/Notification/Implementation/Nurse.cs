using Notification.Contracts;

namespace Notification.Implementation
{
    public class Nurse : INurse
    {
        private string name;
        public List<string> Notifications { get; private set; }

        public Nurse(string name)
        {
            this.name = name;
            this.Notifications = new List<string>();
        }

        public void Update(string message)
        {
            this.Notifications.Add(message);
        }
    }
}