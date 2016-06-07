using System;
using System.Collections.Generic;
using System.Linq;
using MindLink.Recruitment.MyChat.Exporters;
using MindLink.Recruitment.MyChat.Enums;
namespace MindLink.Recruitment.MyChat.Interfaces
{
    public interface IFilter
    {
        /// <summary>
        /// Filters the messages based on the filters parameters
        /// </summary>
        /// <param name="messages">The messages to be filtered</param>
        /// <param name="filterParameter">The parameters for filtering the parameters</param>
        /// <returns>A filtered enumerable that is a subset of the messages parameters</returns>
        IEnumerable<Message> Filter(IEnumerable<Message> messages, string[] filterParameter);
    }
}
