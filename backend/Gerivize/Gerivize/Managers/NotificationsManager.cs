using Gerivize.Models;
using Gerivize.Repositories;

namespace Gerivize.Managers
{
    public class NotificationsManager
    {
        private NotificationRepository _notificationRepository;
        private InstrumentRepository _instrumentRepository;
        private UserRepository _userRepository;

        public NotificationsManager() {
            _notificationRepository = new NotificationRepository();
            _instrumentRepository = new InstrumentRepository();
            _userRepository = new UserRepository();
        }

        public void findIstrumenmtsToCreateNotificationsFor()
        {
            List<Instrument> instruments = _instrumentRepository.getByNextCalibrationDate();
            List<Notification> notifications = _notificationRepository.getNotificationsBetweenDates(DateTime.Today.AddMonths(-3), DateTime.Today);
            List<KeyValuePair<string, Instrument>> instrumentsToCreateNotifications = new List<KeyValuePair<string, Instrument>>();
            string _external = "";
            string _internal = "";
            foreach (Instrument instrument in instruments)
            {
                List<Notification> instrumentNotifications = notifications.Where(n => n.ANumber == instrument.ANumber).ToList();
                DateTime oldDate = DateTime.Today.AddMonths(-3).AddDays(-7);
                instrumentNotifications.ForEach(n =>
                {
                    if (n.CreationDate > oldDate)
                    {
                        oldDate = n.CreationDate;
                    }
                });
                Notification notification = instrumentNotifications.Find(n => n.CreationDate == oldDate);
                /*if (instrument.ExternalCalibration)
                {
                    //_external += instrument.ANumber + " | " + (instrument.NextCalibrationDate - DateTime.Today).Days + "\n";
                    
                    if (notification != null)
                    {
                        if (instrument.NextCalibrationDate >= DateTime.Today.AddMonths(1).AddDays(15) && instrument.NextCalibrationDate <= DateTime.Today.AddMonths(3))
                        {
                            if (notification.CreationDate < DateTime.Today.AddMonths(-1).AddDays(-14) && notification.CreationDate >= DateTime.Today.AddMonths(-3))
                            {
                                //  Send notification
                                Console.WriteLine("1.5 - 3 måneder: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                                instrumentsToCreateNotifications.Add(instrument);
                            }
                        }
                        else if(instrument.NextCalibrationDate > DateTime.Today.AddMonths(1).AddDays(1) && instrument.NextCalibrationDate <= DateTime.Today.AddMonths(1).AddDays(14))
                        {
                            if(notification.CreationDate >= DateTime.Today.AddMonths(-1).AddDays(-14))
                            {
                                //  Send notification
                                Console.WriteLine("1 - 1.5 måneder: "+instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                                instrumentsToCreateNotifications.Add(instrument);
                            }
                        }
                    }
                    else
                    {

                        if (instrument.NextCalibrationDate > DateTime.Today.AddMonths(1).AddDays(1) && instrument.NextCalibrationDate <= DateTime.Today.AddMonths(1).AddDays(1))
                        {
                            //  Send notification
                            Console.WriteLine("1 - 1.5 måneder: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                            instrumentsToCreateNotifications.Add(instrument);
                        }
                        else if (instrument.NextCalibrationDate >= DateTime.Today.AddMonths(1).AddDays(15) && instrument.NextCalibrationDate <= DateTime.Today.AddMonths(3))
                        {
                            
                            //  Send notification
                            Console.WriteLine("1.5 - 3 måneder: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                            instrumentsToCreateNotifications.Add(instrument);
                        }
                    }
                }
                if (notification != null)
                {

                    if (instrument.NextCalibrationDate <= DateTime.Today.AddMonths(1) && instrument.NextCalibrationDate > DateTime.Today.AddDays(7))
                    {
                        if (notification.CreationDate <= DateTime.Today.AddDays(-14))
                        {
                            //  Send notification
                            Console.WriteLine("14 - 30 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                            instrumentsToCreateNotifications.Add(instrument);
                        }
                    }
                    else if (instrument.NextCalibrationDate > DateTime.Today && instrument.NextCalibrationDate <= DateTime.Today.AddDays(7))
                    {
                        if (notification.CreationDate > DateTime.Today && notification.CreationDate <= DateTime.Today.AddDays(-7))
                        {
                            //  Send notification
                            Console.WriteLine("1 - 7 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                            instrumentsToCreateNotifications.Add(instrument);
                        }
                    }
                    else if (instrument.NextCalibrationDate <= DateTime.Today)
                    {
                        if (notification.CreationDate < DateTime.Today)
                        {
                            //  Send notification
                            Console.WriteLine("Overdue: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + notification.CreationDate.Date.ToString());
                            instrumentsToCreateNotifications.Add(instrument);
                        }
                    }
                }
                else
                {

                    if (instrument.NextCalibrationDate > DateTime.Today.AddDays(14) && instrument.NextCalibrationDate <= DateTime.Today.AddMonths(1))
                    {
                        //  Send notification
                        Console.WriteLine("14 - 30 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                        instrumentsToCreateNotifications.Add(instrument);
                    }
                    else if (instrument.NextCalibrationDate >= DateTime.Today.AddDays(7) && instrument.NextCalibrationDate <= DateTime.Today.AddDays(14))
                    {

                        //  Send notification
                        Console.WriteLine("7 - 14 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                        instrumentsToCreateNotifications.Add(instrument);
                    }
                    else if (instrument.NextCalibrationDate >= DateTime.Today && instrument.NextCalibrationDate <= DateTime.Today.AddDays(7))
                    {                     
                        //  Send notification
                        Console.WriteLine("0 - 7 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                        instrumentsToCreateNotifications.Add(instrument);
                    }
                    else if (instrument.NextCalibrationDate < DateTime.Today)
                    {
                        //  Send notification
                        Console.WriteLine("Overdue: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                        instrumentsToCreateNotifications.Add(instrument);
                    }
                }*/

                if(instrument.ExternalCalibration && 
                    (instrument.NextCalibrationDate - DateTime.Today).TotalDays <= (instrument.ExternalCalibration ? 90 : 45) && 
                    (instrument.NextCalibrationDate - DateTime.Today).TotalDays > 30)
                {
                    if(notification != null && (notification.CreationDate - DateTime.Today).TotalDays <= -45)
                    {
                        Console.WriteLine("1 - 3 måneder######: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                        instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                    }
                    else if(notification == null)
                    {
                        Console.WriteLine("1 - 3 måneder: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration);
                        instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                    }
                }else if((instrument.NextCalibrationDate - DateTime.Today).TotalDays <= -7 ||
                    instrument.NextCalibrationDate == DateTime.Today ||
                    (instrument.NextCalibrationDate - DateTime.Today).TotalDays <= 7 ||
                    (instrument.NextCalibrationDate - DateTime.Today).TotalDays <= 14 ||
                    (instrument.NextCalibrationDate - DateTime.Today).TotalDays <= 30){
                    
                    if(notification != null && !notification.HasReacted)
                    {
                        if((instrument.NextCalibrationDate - DateTime.Today).TotalDays >= 14 && (notification.CreationDate - DateTime.Today).TotalDays <= -7)
                        {
                            Console.WriteLine("14 - 30 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays + " | " + (notification.CreationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }else if((instrument.NextCalibrationDate - DateTime.Today).TotalDays >= 7 && (notification.CreationDate - DateTime.Today).TotalDays <= -7)
                        {
                            Console.WriteLine("7 - 14 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays + " | " + (notification.CreationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }else if(((instrument.NextCalibrationDate - DateTime.Today).TotalDays > 0 && (instrument.NextCalibrationDate - DateTime.Today).TotalDays < -7) && (notification.CreationDate - DateTime.Today).TotalDays < 0)
                        {
                            Console.WriteLine("1 - 7 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays + " | " + (notification.CreationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }else if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays >= -7 && (notification.CreationDate - DateTime.Today).TotalDays < 0)
                        {
                            Console.WriteLine("-7 - 0 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays + " | " + (notification.CreationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("everyone", instrument));
                        }else if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays < -7 && (notification.CreationDate - DateTime.Today).TotalDays < 0)
                        {
                            Console.WriteLine("Overdue: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays + " | " + (notification.CreationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                    }
                    else if(notification == null || (notification != null && !notification.HasReacted))
                    {
                        if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays >= 14)
                        {
                            Console.WriteLine("14 - 30 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays >= 7)
                        {
                            Console.WriteLine("7 - 14 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays > 0)
                        {
                            Console.WriteLine("1 - 7 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays >= -7)
                        {
                            Console.WriteLine("-7 - 0 dage: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("everyone", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - DateTime.Today).TotalDays < -7)
                        {
                            Console.WriteLine("Overdue: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays);
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else
                        {
                            Console.WriteLine("Error: " + instrument.ANumber + " | External: " + instrument.ExternalCalibration + " | " + (instrument.NextCalibrationDate - DateTime.Today).TotalDays);
                        }
                    }
                }
            }
            //Console.WriteLine("External:\n"+_external+"\n\n");
            //Console.WriteLine("Internal:\n"+_internal);
            if(instrumentsToCreateNotifications.Count > 0)
            {
                CreateNotifications(instrumentsToCreateNotifications);
            }else
            {
                Console.WriteLine("Everything is up to date");
            }
        }

        public void CreateNotifications(List<KeyValuePair<string, Instrument>> instruments)
        {
            foreach (KeyValuePair<string, Instrument> instrument in instruments)
            {
                _notificationRepository.createNotification(new Notification()
                {
                    Id = Guid.NewGuid(),
                    ANumber = instrument.Value.ANumber,
                    UserId = instrument.Value.UserId ?? _userRepository.getAll().FirstOrDefault().Id,
                    CreationDate = DateTime.Today,
                    HasReacted = false
                });
            }

            //SendEmailNotification(instruments);
        }

        public void SendEmailNotification(List<KeyValuePair<string, Instrument>> instruments)
        {

        }
    }
}
