using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// IloggingService 
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// The General Log method
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);

    }
}
