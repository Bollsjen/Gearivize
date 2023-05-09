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
            //  Finder alle klasser der arver fra SchedulerBaseClass
            var derivedTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(SchedulerBaseClass)));

            foreach (var type in derivedTypes)
            {
                //  Finder alle metoder der benytter sig af SchedulerAttribute attributten
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttributes(typeof(SchedulerAttribute), true).Any());
                foreach (var method in methods)
                {
                    string methodName = method.Name;
                    //  Får MethodInfo på metoden ud fra metode navnet i klassen den har fundet
                    MethodInfo? info = type.GetMethod(methodName);
                    //  Får attribut dataen for den specifikke attribut brug på metoden
                    var cronJobAttribute = method.GetCustomAttribute<SchedulerAttribute>();
                    if (cronJobAttribute != null && info != null)
                    {
                        //  Opretter en instance af klasse typen vi fandt
                        var instance = Activator.CreateInstance(type);
                        //  Opretter et job der kalder metoden der gør brug at attributten
                        var job = new Job(instance.GetType(), info);
                        var manager = new RecurringJobManager();
                        if (instance != null)
                        {
                            //  Opretter et recurring job for metoden der benytter attributten der køres så ofte som var bestemt i attributtens data
                            manager.AddOrUpdate(type.Namespace + "." + type.Name + "." + method.Name, job, cronJobAttribute.Parameter, TimeZoneInfo.Local);
                            Console.WriteLine("Job created");
                        }
                        else
                            Console.WriteLine("Recurring Job Error: Instance was null");
                    }
                    else
                        Console.WriteLine("CronJobAttribute is null");
                }
            }
        }
    }
}
