using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    class ConsoleLogger : ILoggingService
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
