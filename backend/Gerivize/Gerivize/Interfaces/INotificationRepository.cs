using Gerivize.Models;

namespace Gerivize.Interfaces
{
    public interface INotificationRepository
    {
        public List<Notification> getAll();
        public Notification? getById(Guid id);
        public List<Notification> getByUserId(Guid userId);
        public List<Notification> getByANumber(string aNumber);
        public List<Notification> getNotificationsBetweenDates(DateTime fromDate, DateTime toDate);

        public void createNotification(Notification notification);
        public Notification hasReadtedOnNotifcation(Guid id);
    }
}
