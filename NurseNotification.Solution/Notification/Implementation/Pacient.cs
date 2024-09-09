using Notification.Contracts;

namespace Notification.Implementation
{
    public class Pacient : IPacient
    {
        private List<INurse> nurses = new List<INurse>();

        public void AddNurse(INurse nurse)
        {
            this.nurses.Add(nurse);
        }

        public void NotifyNurses(string message)
        {
            foreach (var nurse in this.nurses)
            {
                nurse.Update(message);
            }
        }

        public void RemoveNurse(INurse nurse)
        {
            this.nurses.Remove(nurse);
        }

        public void ChangeCondition(string newCondition)
        {
            NotifyNurses($"The patient's condition changed to: {newCondition}");
        }
    }
}