﻿using System.IO;
using System.Linq;
using System.Collections.Generic;
using MyChat;
using Newtonsoft.Json;
using MindLink.Recruitment.MyChat;
using MindLink.Recruitment.MyChat.Exporters;
using MindLink.Recruitment.MyChat.Interfaces;
using MindLink.Recruitment.MyChat.Enums;
using Microsoft.Practices.Unity;

namespace MindLink.Recruitment.MyChat.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MindLink.Recruitment.MyChat.Filters;
    using MindLink.Recruitment.MyChat.Exceptions;

    /// <summary>
    /// Tests for the <see cref="UserFilter"/>.
    /// </summary>
    [TestClass]
    public class UserFilterTests
    {
        private IEnumerable<Message> ReadMessages()
        {
            UnityContainer container = new UnityContainer();
            container.AddNewExtension<MyChatUnityContainer>();
            IExporter exporter = container.Resolve<IExporter>();

            exporter.ExportConversation("chat.txt", "chat.json");
            Conversation savedConversation;

            using (var reader = new StreamReader(new FileStream("chat.json", FileMode.Open)))
            {
                var serializedConversation = reader.ReadToEnd();
                savedConversation = JsonConvert.DeserializeObject<Conversation>(serializedConversation);
            }


            return savedConversation.messages != null ? savedConversation.messages.ToList() : new List<Message>();
        }

        [TestMethod]
        public void CanFilterBasedOnUserInChat()
        {
            UserFilter filter = new UserFilter();
            var messages = ReadMessages();
            string user = "Bob";

            List<Message> returnMessages = filter.Filter(messages, new string[] { user }).ToList();
            Assert.IsNotNull(returnMessages);
            Assert.AreNotEqual(messages, returnMessages);
            Assert.AreEqual(0, returnMessages.Count(t => t.senderId != user));
            Assert.IsTrue(returnMessages.Count() > 0);
        }

        [TestMethod]
        public void CannotFilterBasedOnNonExistantUser()
        {
            try
            {
                UserFilter filter = new UserFilter();
                var messages = ReadMessages();
                string user = "John";

                List<Message> returnMessages = filter.Filter(messages, new string[] { user }).ToList();
                Assert.IsNotNull(returnMessages);
                Assert.AreNotEqual(messages, returnMessages);
                Assert.IsTrue(returnMessages.Count == 0);
            }
            catch (UserNotInChatException)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod]
        public void CannotFilterBasedOnNullUser()
        {
            UserFilter filter = new UserFilter();
            var messages = ReadMessages();
            string user = null;

            List<Message> returnMessages = filter.Filter(messages.ToList(), new string[] { user }).ToList();
            Assert.IsNotNull(returnMessages);
            CollectionAssert.AreEqual(messages.ToList(), returnMessages);
        }

        [TestMethod]
        public void CannotFilterNullMessages()
        {
            try
            {
                UserFilter filter = new UserFilter();
                filter.Filter(null, new string[] { "1" });
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
