namespace Notification.Contracts
{
    public interface IPacient
    {
        void AddNurse(INurse nurse);

        void RemoveNurse(INurse nurse);

        void NotifyNurses(string message);
    }
}