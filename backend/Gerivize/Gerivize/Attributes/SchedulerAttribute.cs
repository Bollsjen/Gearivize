namespace Gerivize.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SchedulerAttribute : Attribute
    {
        public string Parameter { get; }

        /// <summary>
        /// Attribute/Annotation for starting scheduled jobs on launch with a Cron expression
        /// </summary>
        /// <param name="parameter">Cron expression</param>
        /// <exception cref="ArgumentException">6 parameters in the Cron expression required</exception>
        public SchedulerAttribute(string parameter)
        {
            string[] splitUp = parameter.Split(' ');
            if (splitUp.Length != 6) throw new ArgumentException();
            Parameter = parameter;
        }
    }
}
