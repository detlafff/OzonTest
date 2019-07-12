using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
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

            
            var job = JobBuilder.Create<HelloJob>()
                .Build();
            
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var servise = new CnbApiClient.CnbService(
                new Uri("https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"));
            var ans = servise.GetRawDailyCurrencyByDate(DateTime.Now);
            //ans.ToList().ForEach();
            Console.WriteLine(DateTime.Now);
        }
    }
}