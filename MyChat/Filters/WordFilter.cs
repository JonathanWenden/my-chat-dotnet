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
    public class WordFilter : IFilter
    {
        private string _replacement;
        public WordFilter(string replacement)
        {
            _replacement = replacement;
        }

        public IEnumerable<Message> Filter(IEnumerable<Message> messages, string[] filterParameter)
        {
            if (messages == null)
            {
                throw new NullMessagesException("A value of null was provided for the messages");
            }
            else if (filterParameter == null)
            {
                return messages;
            }
            else
            {
                foreach (string filter in filterParameter)
                {
                   messages = messages.Select(t => replaceContent(t, filter));
                }
                return messages;
            }
        }

        private Message replaceContent(Message m, string filter)
        {
            return new Message(m.timestamp, m.senderId, m.content.Replace(_replacement, filter));
        }
    }
}
