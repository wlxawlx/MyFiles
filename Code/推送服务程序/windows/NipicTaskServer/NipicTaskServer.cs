using System;
using System.Threading;
using Common.Logging;
using Quartz;
using Quartz.Impl;

namespace NipicTask
{

	public class NipicTaskServer : INipicTaskServer
	{
		private readonly ILog logger;
        private Quartz.ISchedulerFactory schedulerFactory;
        private Quartz.IScheduler scheduler;


        public NipicTaskServer()
	    {
	        logger = LogManager.GetLogger(GetType());
	    }


		public virtual void Initialize()
		{
			try
			{				
				schedulerFactory = CreateSchedulerFactory();
				scheduler = GetScheduler();
			}
			catch (Exception e)
			{
				logger.Error("Server initialization failed:" + e.Message, e);
				throw;
			}
		}


	    protected virtual IScheduler GetScheduler()
	    {
	        return schedulerFactory.GetScheduler();
	    }


	    protected virtual IScheduler Scheduler
	    {
	        get { return scheduler; }
	    }


	    protected virtual ISchedulerFactory CreateSchedulerFactory()
	    {
	        return new StdSchedulerFactory();
	    }


		public virtual void Start()
		{
			scheduler.Start();

			try 
			{
				Thread.Sleep(3000);
			} 
			catch (ThreadInterruptedException) 
			{
			}

			logger.Info("Scheduler started successfully");
		}


		public virtual void Stop()
		{
			scheduler.Shutdown(true);
			logger.Info("Scheduler shutdown complete");
		}


	    public virtual void Dispose()
	    {
	        // no-op for now
	    }


	    public virtual void Pause()
	    {
	        scheduler.PauseAll();
	    }


	    public void Resume()
	    {
	        scheduler.ResumeAll();
	    }
	}
}
