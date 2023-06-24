using Gerivize.Attributes;
using Gerivize.Managers;

namespace Gerivize.Schedulers
{
    public class NotificationsScheduler : SchedulerBaseClass
    {
        private readonly NotificationsManager _notificationsManager;

        public NotificationsScheduler(NotificationsManager manager)
        {
            _notificationsManager = manager;
        }

        //[Scheduler("0 0 12 * * SUN")]
        [Scheduler("0 */1 * * * *")]
        public void CheckForCalibrationAndSendNotifications()
        {
            _notificationsManager.findIstrumenmtsToCreateNotificationsFor();
        }
    }
}
