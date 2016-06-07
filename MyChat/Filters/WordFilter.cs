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
    public class WordFilter : IFilter
    {
        private string _replacement;
        /// <summary>
        /// Creates a filter to replace words with the replacement parameter
        /// </summary>
        /// <param name="replacement">The string that filtered words will be replaced with</param>
        public WordFilter(string replacement)
        {
            _replacement = replacement;
        }

        /// <summary>
        /// Filters the messages based on the filters parameters
        /// 
        /// </summary>
        /// <param name="messages">The messages to be filtered</param>
        /// <param name="filterParameter">A paramter array containing words to be hidden, null values will not be filtered</param>
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
            else
            {
                foreach (string filter in filterParameter)
                {
                   messages = messages.Select(t => replaceContent(t, filter));
                }
                return messages;
            }
        }

        /// <summary>
        /// Filters a single message's content based on the filter
        /// </summary>
        /// <param name="m">The source message to be filter</param>
        /// <param name="filter">The string to be replaced</param>
        /// <returns>A message with a filtered content</returns>
        private Message replaceContent(Message m, string filter)
        {
            return new Message(m.timestamp, m.senderId, m.content.Replace(_replacement, filter));
        }
    }
}
