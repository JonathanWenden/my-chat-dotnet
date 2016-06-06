using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindLink.Recruitment.MyChat.Filters;
using MindLink.Recruitment.MyChat.Exceptions;
using MindLink.Recruitment.MyChat.Exporters;
using MyChat;

namespace MindLink.Recruitment.MyChat.Tests
{

    [TestClass]
    public class WordFilterTests
    {
        private List<Message> ReadMessages()
        {
            Exporter exporter = new Exporter();

            exporter.ExportConversation("chat.txt", "chat.json");
            Conversation savedConversation;

            using (var reader = new StreamReader(new FileStream("chat.json", FileMode.Open)))
            {
                var serializedConversation = reader.ReadToEnd();
                savedConversation = JsonConvert.DeserializeObject<Conversation>(serializedConversation);
            }


            return savedConversation != null ? savedConversation.messages.ToList() : new List<Message>();
        }

        [TestMethod]
        public void CanReplaceBasedOnWordInChat()
        {
            string replacement = "*redacted*";
            WordFilter filter = new WordFilter(replacement);
            var messages = ReadMessages();
            string content = "pie";

            List<Message> returnMessages = filter.Filter(messages, new string[] { content }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreNotEqual(messages, returnMessages);
            Assert.IsTrue(returnMessages.Count() > 0);

            foreach (Message m in returnMessages)
            {
                Assert.IsTrue((!m.content.Contains(replacement) && !m.content.Contains(content)) || 
                              (m.content.Contains(replacement) && !m.content.Contains(content)));
                
            }
        }

        [TestMethod]
        public void CannotFilterBasedOnNonExistantWord()
        {
            string replacement = "*redacted*";
            WordFilter filter = new WordFilter(replacement);
            var messages = ReadMessages();
            string content = "asdasdasdasdadas";

            List<Message> returnMessages = filter.Filter(messages, new string[] { content }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreEqual(messages, returnMessages);
        }


        [TestMethod]
        public void CannotFilterBasedOnNullWord()
        {
            string replacement = "*redacted*";
            WordFilter filter = new WordFilter(replacement);
            var messages = ReadMessages();
            string content = null;

            List<Message> returnMessages = filter.Filter(messages, new string[] { content }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreEqual(messages.Count, returnMessages.Count);
            for (int i = 0; i < messages.Count; i++)
            {
                Assert.IsTrue(messages[i].Equals(returnMessages[i]));
            }
        }

        [TestMethod]
        public void CannotFilterNullContent()
        {
            try
            {
                WordFilter filter = new WordFilter("*redacted*");
                filter.Filter(null, new string[] { "1" });
            }
            catch (NullMessagesException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
