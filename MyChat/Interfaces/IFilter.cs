using System;
using System.Collections.Generic;
using System.Linq;
using MindLink.Recruitment.MyChat.Exporters;
namespace MindLink.Recruitment.MyChat.Interfaces
{
    public interface IFilter
    {
        IEnumerable<Message> Filter(IEnumerable<Message> messages, string[] filterParameter);
    }
}
