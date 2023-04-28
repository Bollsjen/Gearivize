using Gerivize.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerivize.Repositories
{
    public class NotificationRepository
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
            return _localContext.Notifications.Include(n => n.User).Include(n => n.Instrument).Where(n => n.CreationDate > fromDate).ToList();
        }

        public void createNotification(Notification notification)
        {
            _localContext.Notifications.Add(notification);
            _localContext.SaveChanges();
        }

        public Notification updateNotification(Notification notification)
        {
            _localContext.Notifications.Update(notification);
            _localContext.SaveChanges();
            return notification;
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
