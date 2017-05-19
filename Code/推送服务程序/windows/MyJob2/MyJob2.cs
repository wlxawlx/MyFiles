using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;

namespace NipicTask
{
    public class MyJob2 : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger("log");
        public void Execute(IJobExecutionContext context)
        {
            MyFun.Start();
        }


        public class MyFun
        {
            public static void Start()//可以在这个方法中写你想要的操作
            {
                ServiceReference1.HPServiceSoapClient wc = new ServiceReference1.HPServiceSoapClient();
                logger.Info("MyJob2      " + wc.accountService("FY030315", "<root><result>1</result></root>"));
            }
        }
    }
}
