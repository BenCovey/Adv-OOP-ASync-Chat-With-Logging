using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// Logging Class
    /// </summary>

    public class TextLogger : ILoggingService
    {
        /// <summary>
        /// Write file, writing to log files
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="Data"></param>
        public void Log(String Data)
        {
            try
            {
                string filepath = "C:/test/TEST" + DateTime.Now.ToString("yyyy-MM-dd-HH") + ".log";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Data);
                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Data);
                    }

                }
            }//end try
            catch(IOException err)
            {
                //TODO
                //Testing, remove for finished assignment
                Console.Write(err);
            }//end catch
        }//end writefile
    }//end logging
}//end main