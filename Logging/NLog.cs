using NLog;

namespace Logging 
{
    public class NLog : ILoggingService
    {
        //Instance NLOG under logger 
        //Followed tutorial from https://brutaldev.com/post/logging-setup-in-5-minutes-with-nlog
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// NLOG Log() Method
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            //Use .Info() --information messages, which are normally enabled in production environment
            logger.Info(message);
        }
    }
}