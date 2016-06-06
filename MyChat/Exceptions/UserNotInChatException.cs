using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindLink.Recruitment.MyChat.Exceptions
{
    public class UserNotInChatException : Exception
    {
        public UserNotInChatException(string message)
            : base(message)
        {
        }
    }
}
