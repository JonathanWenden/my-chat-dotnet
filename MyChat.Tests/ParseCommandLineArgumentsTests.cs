using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindLink.Recruitment.MyChat.Structs;
using MindLink.Recruitment.MyChat.Enums;
using MindLink.Recruitment.MyChat.Exporters;

namespace MindLink.Recruitment.MyChat.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class ParseCommandLineArgumentsTests
    {
        private CommandLineArgumentParser parser;

        [TestMethod]
        public void CanParseFilePathsOnly()
        {
            parser = new CommandLineArgumentParser();

            string[] args = { "C:\\Logs.txt", "C:\\Logs.json" };

            var s = parser.ParseCommandLineArguments(args);
            Assert.AreEqual("C:\\Logs.txt", s.inputFilePath);
            Assert.AreEqual("C:\\Logs.json", s.outputFilePath);
            Assert.IsNotNull(s.commandLineArguments);
            Assert.IsTrue(s.commandLineArguments.Count == 0);
        }


        [TestMethod]
        public void CanParseCommandLineArgumentsWithUserFilter()
        {
            parser = new CommandLineArgumentParser();

            string[] args = { "C:\\Logs.txt", "C:\\Logs.json", "-U", "bob"};

            var s = parser.ParseCommandLineArguments(args);
            Assert.AreEqual("C:\\Logs.txt", s.inputFilePath);
            Assert.AreEqual("C:\\Logs.json", s.outputFilePath);
            Assert.IsNotNull(s.commandLineArguments);
            Assert.IsTrue(s.commandLineArguments.Count == 1);
            Assert.AreEqual(CommandLineArgumentType.UserFilter, s.commandLineArguments[0].ArgumentType);
            Assert.AreEqual(1, s.commandLineArguments[0].AdditionalParameters.Count);
            Assert.AreEqual("bob", s.commandLineArguments[0].AdditionalParameters[0]);
        }

        [TestMethod]
        public void CanParseCommandLineArgumentsWithMessageFilter()
        {
            parser = new CommandLineArgumentParser();

            string[] args = { "C:\\Logs.txt", "C:\\Logs.json", "-M", "pie" };

            var s = parser.ParseCommandLineArguments(args);
            Assert.AreEqual("C:\\Logs.txt", s.inputFilePath);
            Assert.AreEqual("C:\\Logs.json", s.outputFilePath);
            Assert.IsNotNull(s.commandLineArguments);
            Assert.IsTrue(s.commandLineArguments.Count == 1);
            Assert.AreEqual(CommandLineArgumentType.MessageFilter, s.commandLineArguments[0].ArgumentType);
            Assert.AreEqual(1, s.commandLineArguments[0].AdditionalParameters.Count);
            Assert.AreEqual("pie", s.commandLineArguments[0].AdditionalParameters[0]);
        }
        
        [TestMethod]
        public void CanParseCommandLineArgumentsWithBothFilters()
        {
            parser = new CommandLineArgumentParser();

            string[] args = { "C:\\Logs.txt", "C:\\Logs.json", "-M", "pie", "-U", "bob" };

            var s = parser.ParseCommandLineArguments(args);
            Assert.AreEqual("C:\\Logs.txt", s.inputFilePath);
            Assert.AreEqual("C:\\Logs.json", s.outputFilePath);
            Assert.IsNotNull(s.commandLineArguments);
            Assert.IsTrue(s.commandLineArguments.Count == 2);

            Assert.AreEqual(CommandLineArgumentType.MessageFilter, s.commandLineArguments[0].ArgumentType);
            Assert.AreEqual(1, s.commandLineArguments[0].AdditionalParameters.Count);
            Assert.AreEqual("pie", s.commandLineArguments[0].AdditionalParameters[0]);
            
            Assert.AreEqual(CommandLineArgumentType.UserFilter, s.commandLineArguments[1].ArgumentType);
            Assert.AreEqual(1, s.commandLineArguments[1].AdditionalParameters.Count);
            Assert.AreEqual("bob", s.commandLineArguments[1].AdditionalParameters[0]);
        }

        [TestMethod]
        public void CannotParseNoCommandLineParameters()
        {
            parser = new CommandLineArgumentParser();

            string[] args = { };

            var s = parser.ParseCommandLineArguments(args);
            Assert.AreEqual("", s.inputFilePath);
            Assert.AreEqual("", s.outputFilePath);
            Assert.IsNotNull(s.commandLineArguments);
            Assert.IsTrue(s.commandLineArguments.Count == 0);
        }
    }
}
