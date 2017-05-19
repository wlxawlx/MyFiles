using System;
using Common.Logging;

namespace NipicTask
{
    /// <summary>
    /// Factory class to create Quartz server implementations from.
    /// </summary>
    public class NipicTaskServerFactory
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (NipicTaskServerFactory));

        /// <summary>
        /// Creates a new instance of an Quartz.NET server core.
        /// </summary>
        /// <returns></returns>
        public static INipicTaskServer CreateServer()
        {
            string typeName = Configuration.ServerImplementationType;

            Type t = Type.GetType(typeName, true);

            logger.Debug("Creating new instance of server type '" + typeName + "'");
            INipicTaskServer retValue = (INipicTaskServer)Activator.CreateInstance(t);
            logger.Debug("Instance successfully created");
            return retValue;
        }
    }
}