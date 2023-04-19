using Gerivize.Interfaces;
using Gerivize.Models;

namespace Gerivize.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly GearivizeLocalContext _localContext;

        public NotificationRepository()
        {
            _localContext = new GearivizeLocalContext();
        }

        public List<Notification> getAll()
        {
            return _localContext.Notifications.ToList();
        }
        public Notification? getById(Guid id)
        {
            return _localContext.Notifications.ToList().Find(n => n.Id == id);
        }

        public List<Notification> getByANumber(string aNumber)
        {
            return _localContext.Notifications.Where(n => n.ANumber == aNumber).ToList();
        }

        public List<Notification> getByUserId(Guid userId)
        {
            return _localContext.Notifications.Where(n => userId == n.UserId).ToList();
        }

        public List<Notification> getNotificationsBetweenDates(DateTime fromDate, DateTime toDate)
        {
            return _localContext.Notifications.Where(n => n.CreationDate > fromDate && n.CreationDate < toDate).ToList();
        }

        public void createNotification(Notification notification)
        {
            _localContext.Notifications.Add(notification);
            _localContext.SaveChanges();
        }

        public Notification hasReadtedOnNotifcation(Guid id)
        {
            Notification notification = getById(id);
            notification.HasReacted = true;
            _localContext.Update(notification);
            _localContext.SaveChanges(true);
            return notification;
        }
    }
}
