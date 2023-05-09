using Gerivize.Attributes;
using Gerivize.Managers;

namespace Gerivize.Schedulers
{
    public class NotificationsScheduler : SchedulerBaseClass
    {
        private readonly NotificationsManager _notificationsManager;

        public NotificationsScheduler()
        {
            _notificationsManager = new NotificationsManager();
        }

        [Scheduler("45 11 * * *")]
        public void CheckForCalibrationAndSendNotifications()
        {
            _notificationsManager.findIstrumenmtsToCreateNotificationsFor();
        }
    }
}
