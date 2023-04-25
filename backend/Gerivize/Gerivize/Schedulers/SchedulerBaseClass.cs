using Hangfire.Common;
using Hangfire.MemoryStorage.Entities;
using Hangfire;
using System.Reflection;
using Gerivize.Attributes;

namespace Gerivize.Schedulers
{
    public class SchedulerBaseClass
    {
        public static void InittializeSchedulers()
        {
            var derivedTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(SchedulerBaseClass)));

            foreach (var type in derivedTypes)
            {
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttributes(typeof(SchedulerAttribute), true).Any());
                foreach (var method in methods)
                {
                    string methodName = method.Name;
                    MethodInfo? info = type.GetMethod(methodName);
                    var cronJobAttribute = method.GetCustomAttribute<SchedulerAttribute>();
                    Console.WriteLine(type.GetMethod(method.Name).GetType());
                    if (cronJobAttribute != null && info != null)
                    {
                        var instance = Activator.CreateInstance(type);
                        var job = new Job(instance.GetType(), info);
                        var manager = new RecurringJobManager();
                        if (instance != null)
                        {
                            manager.AddOrUpdate(type.GetType().Name + "." + method.Name, job, cronJobAttribute.Parameter);
                            Console.WriteLine("Job created");
                        }
                        else
                            Console.WriteLine("Error");
                    }
                    else
                        Console.WriteLine("CronJobAttribute is null");
                }
            }
        }
    }
}
