using System;
using System.IO;
using System.Text;
using MindLink.Recruitment.MyChat.Common.Interfaces;

namespace MindLink.Recruitment.MyChat.Common
{
    public class Logger : ILogger
    {
        private string _logLocation;

        /// <summary>
        /// Initializes the logger, if the folder provided does not exist then it is created.
        /// </summary>
        /// <param name="logLocation">A valid file path of the log</param>
        public void IntitializeLogger(string logLocation)
        {
            if (logLocation != "")
            {
                _logLocation = logLocation;
                if (!Directory.Exists(Path.GetDirectoryName(_logLocation)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_logLocation));
                }
            }            
        }

        /// <summary>
        /// Writes a string message out to the log file 
        /// </summary>
        /// <param name="message">The log message to be written to the log file</param>
        public void Log(string message)
        {
            message = string.Format("{0} | {1}", DateTime.Now, message);
            using (StreamWriter fs = new StreamWriter(_logLocation,true))
            {
                fs.WriteLine(message);
            }            
        }

        /// <summary>
        /// Writes the details of an exception to the log file. If the log files creates 
        /// </summary>
        /// <param name="ex"></param>
        public void Log(Exception ex)
        {
            string message = string.Format("{0} | {1} | {2} | {3}", DateTime.Now, ex.Message, ex.StackTrace, ex.InnerException);
            Log(message);
        }
    }
}
