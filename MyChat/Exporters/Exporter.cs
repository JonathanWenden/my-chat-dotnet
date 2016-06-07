using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Configuration;
using MindLink.Recruitment.MyChat;
using Newtonsoft.Json;
using MindLink.Recruitment.MyChat.Common.Interfaces;
using MindLink.Recruitment.MyChat.Common;
using MindLink.Recruitment.MyChat.Interfaces;
using MindLink.Recruitment.MyChat.Enums;
using MindLink.Recruitment.MyChat.Filters;

namespace MindLink.Recruitment.MyChat.Exporters
{
    public class Exporter : IExporter
    {
        private ILogger _logger;
        private Dictionary<IFilter, List<string>> _filters;

        public Exporter(ILogger logger)
        {
            _logger = logger;
            _logger.IntitializeLogger(ConfigurationManager.AppSettings["LogFile"]);
            _filters = new Dictionary<IFilter, List<string>>();
        }

        /// <summary>
        /// Adds a filter to the Exporter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="parameters"></param>
        public void AddFilter(IFilter filter, List<string> parameters)
        {
            _filters.Add(filter, parameters);
        }

        /// <summary>
        /// Exports the conversation at <paramref name="inputFilePath"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="inputFilePath">
        /// The input file path.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when a path is invalid.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something bad happens.
        /// </exception>
        public void ExportConversation(string inputFilePath, string outputFilePath)
        {
            Conversation conversation = this.ReadConversation(inputFilePath);
            foreach (KeyValuePair<IFilter, List<string>> pair in _filters)
            {
                conversation.messages = pair.Key.Filter(conversation.messages, pair.Value.ToArray());
            }
            this.WriteConversation(conversation, outputFilePath);

            Console.WriteLine("Conversation exported from '{0}' to '{1}'", inputFilePath, outputFilePath);
        }

        /// <summary>
        /// Helper method to read the conversation from <paramref name="inputFilePath"/>.
        /// </summary>
        /// <param name="inputFilePath">
        /// The input file path.
        /// </param>
        /// <returns>
        /// A <see cref="Conversation"/> model representing the conversation.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input file could not be found.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something else went wrong.
        /// </exception>
        private Conversation ReadConversation(string inputFilePath)
        {
            try
            {
                var reader = new StreamReader(new FileStream(inputFilePath, FileMode.Open, FileAccess.Read),
                    Encoding.ASCII);

                string conversationName = reader.ReadLine();
                var messages = new List<Message>();

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split(' ');
                    string message = "";
                    for (int i = 2; i < split.Length; i++)
                    {
                        message += i == 2 ? split[i] : " " + split[i];
                    }

                    messages.Add(new Message(DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(split[0])), split[1], message));
                    string log = string.Format("Message added: Timestamp: {0}, Username: {1}, Message: {2}", split[0], split[1], message);
                    _logger.Log(log);
                }

                return new Conversation(conversationName, messages);
            }
            catch (ArgumentException ex)
            {
                _logger.Log(ex);
                return new Conversation("", new List<Message>());
            }
            catch (FileNotFoundException ex)
            {
                _logger.Log(ex);
                return new Conversation("", new List<Message>());
            }
            catch (IOException ex)
            {
                _logger.Log(ex);
                return new Conversation("", new List<Message>());
            }
        }

        /// <summary>
        /// Helper method to write the <paramref name="conversation"/> as JSON to <paramref name="outputFilePath"/>.
        /// </summary>
        /// <param name="conversation">
        /// The conversation.
        /// </param>
        /// <param name="outputFilePath">
        /// The output file path.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when there is a problem with the <paramref name="outputFilePath"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown when something else bad happens.
        /// </exception>
        private void WriteConversation(Conversation conversation, string outputFilePath)
        {
            try
            {
                var writer = new StreamWriter(new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite));

                var serialized = JsonConvert.SerializeObject(conversation, Formatting.Indented);

                writer.Write(serialized);

                writer.Flush();

                writer.Close();
            }
            catch (SecurityException ex)
            {
                _logger.Log(ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Log(ex);
            }
            catch (IOException ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
