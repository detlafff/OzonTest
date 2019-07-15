using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Quartz;
using Quartz.Impl;

namespace CurrentExchangeLoaderScheduler
{
    public class Scheduler
    {
        public static async void Start()
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            var factory = new StdSchedulerFactory(props);

            
            var scheduler = await factory.GetScheduler();
            await scheduler.Start();

            
            var job = JobBuilder.Create<ExchangeRateRequestJob>()
                .Build();
            
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(Settings.Instance.cronSchedule)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}