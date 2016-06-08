using System;
using System.Collections.Generic;
using MindLink.Recruitment.MyChat;
using MindLink.Recruitment.MyChat.Interfaces;
using MindLink.Recruitment.MyChat.Exporters;

namespace MindLink.Recruitment.MyChat.Interfaces
{
    public interface IExporter
    {
        /// <summary>
        /// Adds a filter to the Exporter
        /// </summary>
        /// <param name="filter">The filter to be added</param>
        /// <param name="parameters">The filter's parameters provided through the command line arguments</param>
        void AddFilter(IFilter filter, List<string> parameters);
        /// <summary>
        /// Exports the 
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        void ExportConversation(string inputFilePath, string outputFilePath);
    }
}
