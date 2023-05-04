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

        public NotificationsManager() {
            _notificationRepository = new NotificationRepository();
            _instrumentRepository = new InstrumentRepository();
            _userRepository = new UserRepository();
            _instrumentsManager = new InstrumentsManager();
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

            SendEmailNotification(instruments);
        }

        public void SendEmailNotification(List<KeyValuePair<string, Instrument>> instruments)
        {
            //List<User> users = _userRepository.getAll();
            //List<Instrument> emailEveryone = new List<Instrument>();
            //List<KeyValuePair<User, Instrument>> emailResponsibles = new List< KeyValuePair < User, Instrument>> ();

            //instruments.ForEach(item =>
            //{
            //    if(item.Key == "responsible")
            //    {
            //        emailResponsibles.Add(new KeyValuePair<User, Instrument>(item.Value.User, item.Value));
            //    }else if(item.Key == "everyone")
            //    {
            //        emailEveryone.Add(item.Value);
            //    }
            //});

            //Console.WriteLine("Everyone:");
            //emailEveryone.ForEach(item =>
            //{
            //    Console.WriteLine("\t"+item);
            //});


            //Console.WriteLine("\n\nResponsibles:");
            //users.ForEach(user =>
            //{
            //    Console.WriteLine(user.Name);
            //    string message = "<ul>";
            //    emailResponsibles.ForEach(item =>
            //    {
            //        if (item.Key.Id == user.Id)
            //        {
            //            message += $"<li>{item.Value.ANumber} which is scheduled for {item.Value.NextCalibrationDate.ToString("dd/MM/yy")}</li>";
            //        }
            //    });
            //    message += "</ul>";
            //    if (message != "<ul></ul>")
            //    {
            //        _instrumentsManager.SendCalibrationDueEmails(message, user);
            //        Console.WriteLine(message);
            //    }
            //});

            //ChatGPT attempt #1
            //Console.WriteLine("\n\nResponsibles:");
            //emailResponsibles.ForEach(item =>
            //{
            //    Console.WriteLine(item.Key.Name);
            //    string message = $"<li>{item.Value.ANumber} which is scheduled for {item.Value.NextCalibrationDate.ToString("dd/MM/yy")}</li>";
            //    if (item.Value.User != null && item.Value.User.Email != null)
            //    {
            //        _instrumentsManager.SendCalibrationDueEmails(message, item.Value.User);
            //        Console.WriteLine(message);
            //    }
            //    if (item.Value.User2 != null && item.Value.User2.Email != null && item.Value.User2.Id != item.Value.User.Id)
            //    {
            //        _instrumentsManager.SendCalibrationDueEmails(message, item.Value.User2);
            //        Console.WriteLine(message);
            //    }
            //});

            List<User> users = _userRepository.getAll();
            List<Instrument> emailEveryone = new List<Instrument>();
            List<KeyValuePair<User, Instrument>> emailPrimaryResponsibles = new List<KeyValuePair<User, Instrument>>();
            List<KeyValuePair<User, Instrument>> emailSecondaryResponsibles = new List<KeyValuePair<User, Instrument>>();

            instruments.ForEach(item =>
            {
                if (item.Key == "responsible")
                {
                    if (item.Value.User != null)
                    {
                        emailPrimaryResponsibles.Add(new KeyValuePair<User, Instrument>(item.Value.User, item.Value));
                        if (item.Value.User2 != null)
                        {
                            emailSecondaryResponsibles.Add(new KeyValuePair<User, Instrument>(item.Value.User2, item.Value));
                        }
                    }
                    else if (item.Value.User2 != null)
                    {
                        emailSecondaryResponsibles.Add(new KeyValuePair<User, Instrument>(item.Value.User2, item.Value));
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


            Console.WriteLine("\n\nPrimary Responsibles:");
            users.ForEach(user =>
            {
                Console.WriteLine(user.Name);
                string message = "<ul>";
                emailPrimaryResponsibles.ForEach(item =>
                {
                    if (item.Key.Id == user.Id)
                    {
                        message += $"<li>{item.Value.ANumber} which is scheduled for {item.Value.NextCalibrationDate.ToString("dd/MM/yy")}</li>";
                    }
                });
                message += "</ul>";
                if (message != "<ul></ul>")
                {
                    _instrumentsManager.SendCalibrationDueEmails(message, user);
                    Console.WriteLine(message);
                }
            });

            Console.WriteLine("\n\nSecondary Responsibles:");
            users.ForEach(user =>
            {
                Console.WriteLine(user.Name);
                string message = "<ul>";
                emailSecondaryResponsibles.ForEach(item =>
                {
                    if (item.Key.Id == user.Id)
                    {
                        message += $"<li>{item.Value.ANumber} which is scheduled for {item.Value.NextCalibrationDate.ToString("dd/MM/yy")}</li>";
                    }
                });
                message += "</ul>";
                if (message != "<ul></ul>")
                {
                    _instrumentsManager.SendCalibrationDueEmails(message, user);
                    Console.WriteLine(message);
                }
            });
        }
    }
}
