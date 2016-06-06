using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindLink.Recruitment.MyChat.Interfaces;
using MindLink.Recruitment.MyChat.Exceptions;
using MindLink.Recruitment.MyChat.Exporters;

namespace MindLink.Recruitment.MyChat.Filters
{
    public class MessageFilter : IFilter
    {
        public IEnumerable<Message> Filter(IEnumerable<Message> messages, string[] filterParameter)
        {
            if (messages == null)
            {
                throw new NullMessagesException("A value of null was provided for the messages");
            }
            else if (filterParameter == null || filterParameter[0] == null)
            {
                return new List<Message>();
            }
            else if (filterParameter.Length == 0)
            {
                return messages;
            }
            else if (messages.Select(t => t.content == filterParameter[0]).Count() > 0)
            {
                return messages.Where(t => t.content.Contains(filterParameter[0]));
            }
            else
            {
                throw new MessageNotFoundException("No message was found containing the specified message");
            }
            
        }
    }
}
