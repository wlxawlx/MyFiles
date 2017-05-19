using System;
using Quartz;
using Common.Logging;


namespace NipicTask
{
    public class TestJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestJob));
        public void Execute(IJobExecutionContext context)
        {
            logger.Info("TestJob running...");
            SendEmail.Start();
            logger.Info("TestJob run finished.");
        }
    }

    public class SendEmail
    {
        public static void Start()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}
