using System;
using Quartz;
using Common.Logging;


namespace NipicTask
{
    public class SendEmailJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SendEmailJob));
        public void Execute(IJobExecutionContext context)
        {
            logger.Info("SendEmailJob running...");
            SendEmail.Start();
            logger.Info("SendEmailJob run finished.");
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
