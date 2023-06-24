using Hangfire.Common;
using Hangfire.MemoryStorage.Entities;
using Hangfire;
using System.Reflection;
using Gerivize.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gerivize.Managers;

namespace Gerivize.Schedulers
{
    public class SchedulerController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SchedulerController> _logger;

        public SchedulerController(IServiceProvider serviceProvider, ILogger<SchedulerController> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void InitializeSchedulers()
        {
            // Retrieve the ILogger<NotificationsScheduler> from the service provider
            ILogger<NotificationsScheduler> notificationsLogger = _serviceProvider.GetRequiredService<ILogger<NotificationsScheduler>>();

            // Retrieve the NotificationsManager from the service provider
            NotificationsManager notificationsManager = _serviceProvider.GetRequiredService<NotificationsManager>();

            // Finder alle klasser der arver fra SchedulerBaseClass
            var derivedTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(SchedulerBaseClass)));

            foreach (var type in derivedTypes)
            {
                // Finder alle metoder der benytter sig af SchedulerAttribute attributten
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttributes(typeof(SchedulerAttribute), true).Any());

                foreach (var method in methods)
                {
                    string methodName = method.Name;
                    // Får MethodInfo på metoden ud fra metode navnet i klassen den har fundet
                    MethodInfo? info = type.GetMethod(methodName);
                    // Får attribut dataen for den specifikke attribut brug på metoden
                    var cronJobAttribute = method.GetCustomAttribute<SchedulerAttribute>();

                    if (cronJobAttribute != null && info != null)
                    {
                        try
                        {
                            // Opretter en instance af klasse typen vi fandt og injicerer afhængigheder
                            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, type);
                            // Opretter et job der kalder metoden der gør brug af attributten
                            var job = new Job(instance.GetType(), info);
                            var manager = new RecurringJobManager();

                            if (instance != null)
                            {
                                // Opretter et recurring job for metoden der benytter attributten
                                // og køres så ofte som bestemt i attributtens data
                                manager.AddOrUpdate(type.Namespace + "." + type.Name + "." + method.Name, job, cronJobAttribute.Parameter, TimeZoneInfo.Local);
                                _logger.LogInformation("Job created");
                            }
                            else
                            {
                                _logger.LogError("Recurring Job Error: Instance was null");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to create job instance");
                        }
                    }
                    else
                    {
                        _logger.LogError("CronJobAttribute is null");
                    }
                }
            }
        }
    }
}
