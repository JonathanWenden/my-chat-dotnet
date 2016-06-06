﻿using System;
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
    public class ContentFilterTests
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
        public void CanFilterBasedOnMessageInChat()
        {
            MessageFilter filter = new MessageFilter();
            var messages = ReadMessages();
            string content = "pie";

            List<Message> returnMessages = filter.Filter(messages, new string[] { content }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreNotEqual(messages, returnMessages);
            Assert.IsTrue(returnMessages.Count() > 0);
        }

        [TestMethod]
        public void CannotFilterBasedOnNonExistantWord()
        {
            MessageFilter filter = new MessageFilter();
            var messages = ReadMessages();
            string content = "Non-Existant";

            List<Message> returnMessages = filter.Filter(messages, new string[] { content }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreNotEqual(messages, returnMessages);
            Assert.IsTrue(returnMessages.Count() == 0);
        }


        [TestMethod]
        public void CannotFilterBasedOnNullWord()
        {
            MessageFilter filter = new MessageFilter();
            var messages = ReadMessages();
            string content = null;
            
            List<Message> returnMessages = filter.Filter(messages, new string[] { content }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreNotEqual(messages, returnMessages);
            Assert.IsTrue(returnMessages.Count() == 0);
        }

        [TestMethod]
        public void CannotFilterNullContent()
        {
            try
            {
                UserFilter filter = new UserFilter();
                filter.Filter(null, new string[] { "1" });
                Assert.IsFalse(true);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (NullMessagesException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
