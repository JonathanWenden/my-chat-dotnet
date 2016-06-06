using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindLink.Recruitment.MyChat.Common.Interfaces
{
    public interface ILogger
    {
        void IntitializeLogger(string logLocation);

        void Log(string message);
        void Log(Exception ex);
    }
}
