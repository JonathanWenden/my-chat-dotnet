using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindLink.Recruitment.MyChat.Interfaces;
using MindLink.Recruitment.MyChat.Exceptions;
using MindLink.Recruitment.MyChat.Exporters;
using MindLink.Recruitment.MyChat.Enums;

namespace MindLink.Recruitment.MyChat.Filters
{
    public class UserFilter : IFilter
    {
        /// <summary>
        /// Filters the messages based on the filters parameters
        /// </summary>
        /// <param name="messages">The messages to be filtered</param>
        /// <param name="filterParameter">A paramter array containing a single parameter, null values will not be filtered</param>
        /// <returns>A filtered enumerable that is a subset of the messages parameters</returns>
        public IEnumerable<Message> Filter(IEnumerable<Message> messages, string[] filterParameter)
        {
            if (messages == null)
            {
                throw new NullMessagesException("A value of null was provided for the messages");
            }
            else if (filterParameter == null || filterParameter.All(t => t == null) || filterParameter.Length == 0)
            {
                return messages;
            }
            else if (!messages.Any(t => filterParameter.Contains(t.senderId)))
            {
                return new List<Message>();
            }
            else if (messages.Any(t => filterParameter.Contains(t.senderId)))
            {
                return messages.Where(t => filterParameter.Contains(t.senderId));
            }
            else
            {
                return messages;
            }
        }
    }
}
