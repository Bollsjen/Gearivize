using Gerivize.Models;
using Gerivize.Repositories;

namespace Gerivize.Managers
{
    public class NotificationsManager
    {
        private NotificationRepository _notificationRepository;
        private InstrumentRepository _instrumentRepository;
        private UserRepository _userRepository;
        private InstrumentsManager _instrumentsManager;

        public NotificationsManager(InstrumentsManager instrumentManager) {
            _notificationRepository = new NotificationRepository();
            _instrumentRepository = new InstrumentRepository();
            _userRepository = new UserRepository();
            _instrumentsManager = instrumentManager;
        }

        public void findIstrumenmtsToCreateNotificationsFor()
        {
            List<Instrument> instruments = _instrumentRepository.getByNextCalibrationDate();
            List<Notification> notifications = _notificationRepository.getNotificationsBetweenDates(DateTime.Today.AddMonths(-3), DateTime.Today);
            List<KeyValuePair<string, Instrument>> instrumentsToCreateNotifications = new List<KeyValuePair<string, Instrument>>();
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

                DateTime todayWeekAhead = DateTime.Today.AddDays(7);

                if (instrument.ExternalCalibration && 
                    (instrument.NextCalibrationDate - todayWeekAhead).TotalDays <= (instrument.ExternalCalibration ? 90 : 45) && 
                    (instrument.NextCalibrationDate - todayWeekAhead).TotalDays > 30)
                {
                    if(notification != null && (notification.CreationDate - DateTime.Today).TotalDays <= -45)
                    {
                        instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                    }
                    else if(notification == null)
                    {
                        instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                    }                 
                }else{
                    
                    if (notification == null)
                    {
                        if ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays >= 14)
                        {                        
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays >= 7)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays > 0)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays >= -7)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("everyone", instrument));
                        }
                        else if ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays < -7)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }                        
                    }
                    else if(notification != null || (notification != null && !notification.HasReacted))
                    {
                        if(!notification.HasReacted && (instrument.NextCalibrationDate - todayWeekAhead).TotalDays >= 14 && (notification.CreationDate - DateTime.Today).TotalDays <= -14)
                        {                           
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }else if(!notification.HasReacted && ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays >= 7 && (instrument.NextCalibrationDate - DateTime.Today).TotalDays < -14) && (notification.CreationDate - DateTime.Today).TotalDays <= -7)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }else if(!notification.HasReacted && ((instrument.NextCalibrationDate - todayWeekAhead).TotalDays > 0 && (instrument.NextCalibrationDate - DateTime.Today).TotalDays < -7) && (notification.CreationDate - DateTime.Today).TotalDays < 0)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }else if (((instrument.NextCalibrationDate - todayWeekAhead).TotalDays >= -7 && (instrument.NextCalibrationDate - todayWeekAhead).TotalDays <= 0) && (notification.CreationDate - DateTime.Today).TotalDays < 0)
                        {
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("everyone", instrument));
                        }else if (!notification.HasReacted && (instrument.NextCalibrationDate - todayWeekAhead).TotalDays < -7 && (notification.CreationDate - DateTime.Today).TotalDays < 0)
                        {                           
                            instrumentsToCreateNotifications.Add(new KeyValuePair<string, Instrument>("responsible", instrument));
                        }
                    }
                }
            }
            if(instrumentsToCreateNotifications.Count > 0)
            {
                CreateNotifications(instrumentsToCreateNotifications);
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now}] Everything is up to date");
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

            SendEmailNotification(instruments);
        }

        public void SendEmailNotification(List<KeyValuePair<string, Instrument>> instruments)
        {
            List<User> users = _userRepository.getAll();
            List<Instrument> emailEveryone = new List<Instrument>();
            List<KeyValuePair<User, Instrument>> emailResponsibles = new List<KeyValuePair<User, Instrument>>();

            instruments.ForEach(item =>
            {
                if (item.Key == "responsible")
                {
                    emailResponsibles.Add(new KeyValuePair<User, Instrument>(item.Value.User, item.Value));
                    if (item.Value.User2 != null)
                    {
                        emailResponsibles.Add(new KeyValuePair<User, Instrument>(item.Value.User2, item.Value));
                    }
                }
                else if (item.Key == "everyone")
                {
                    emailEveryone.Add(item.Value);
                }
            });

            Console.WriteLine("Everyone:");
            emailEveryone.ForEach(item =>
            {
                Console.WriteLine("\t" + item);
            });


            Console.WriteLine("\n\nResponsibles:");
            users.ForEach(user =>
            {
                Console.WriteLine(user.Name);
                string table = "<table border='1'><tr><th>ID</th><th>Instrument</th><th>Manufacturer</th><th>Type</th><th>Test</th><th>Scheduled</th></tr>";
                emailResponsibles.ForEach(item =>
                {
                    if (item.Key.Id == user.Id)
                    {
                        table += $"<tr><td>{item.Value.ANumber}</td><td>{item.Value.InstrumentName}</td><td>{item.Value.Manufacturer}</td><td>{item.Value.Type}</td><td>{item.Value.Test}</td><td>{item.Value.NextCalibrationDate.ToString("dd/MM/yy")}</td></tr>";
                    }
                });
                emailEveryone.ForEach(item =>
                {
                    table += $"<tr><td>{item.ANumber}</td><td>{item.InstrumentName}</td><td>{item.Manufacturer}</td><td>{item.Type}</td><td>{item.Test}</td><td>{item.NextCalibrationDate.ToString("dd/MM/yy")}</td></tr>";
                });
                table += "</table>";
                if (table != "<table border='1'><tr><th>ID</th><th>Instrument</th><th>Manufacturer</th><th>Type</th><th>Test</th><th>Scheduled</th></tr></table>")
                {
                    _instrumentsManager.SendCalibrationDueEmails(table, user);
                    Console.WriteLine(table);
                }
            });
        }
    }
}
