using System;
using System.IO;

namespace MindLink.Recruitment.MyChat.Common.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MindLink.Recruitment.MyChat.Common.Interfaces;
    using MindLink.Recruitment.MyChat.Common;

    [TestClass]
    public class LoggerTests
    {
        private string errorFilePath;
        private string logFilePath;
        private ILogger _logger;

        public LoggerTests()
        {

            _logger = new Logger();
            errorFilePath = Path.GetFullPath(string.Format(@"C:\Logs\{0}\{1}\{2}\Log-Errors.txt",
                                                DateTime.Now.ToString("yyyy"),
                                                DateTime.Now.ToString("MMM"),
                                                DateTime.Now.Date.ToString("dd")));
            logFilePath = Path.GetFullPath(string.Format(@"C:\Logs\{0}\{1}\{2}\Log-Logs.txt",
                                                DateTime.Now.ToString("yyyy"),
                                                DateTime.Now.ToString("MMM"),
                                                DateTime.Now.Date.ToString("dd")));
        }

        [TestMethod]
        public void CanInitializeLogger()
        {
            try
            {
                _logger.IntitializeLogger(errorFilePath);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public void CanWriteExceptionToLog()
        {
            _logger.IntitializeLogger(errorFilePath);
            Exception ex = new Exception("Test Exception");
            _logger.Log(ex);

            Assert.IsTrue(File.Exists(errorFilePath));
            string fileData = File.ReadAllText(errorFilePath);
            Assert.AreNotEqual("", fileData);
        }

        [TestMethod]
        public void CanWriteStringToLog()
        {
            _logger.IntitializeLogger(logFilePath);
            _logger.Log("Test Log");

            Assert.IsTrue(File.Exists(logFilePath));
            string[] fileData = File.ReadAllLines(logFilePath);
            string lastLine = fileData[fileData.Length - 1];
            Assert.IsTrue(lastLine.EndsWith("| Test Log"));
        }

        [TestMethod]
        public void CannotWriteToInvalidFile()
        {
            try
            {
                _logger.IntitializeLogger("lkdasnfljkdsnfkljsn");
                _logger.Log("Test");
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CannotWriteToEmptyFile()
        {
            try
            {
                _logger.IntitializeLogger("");
                _logger.Log("Test");
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }
        
        [TestMethod]
        public void CannotWriteToNullFile()
        {
            try
            {
                _logger.IntitializeLogger(null);
                _logger.Log("Test");
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
