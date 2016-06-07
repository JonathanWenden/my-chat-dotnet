namespace MyChat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;
    using System.Configuration;
    using System.Text;
    using MindLink.Recruitment.MyChat.Exporters;
    using MindLink.Recruitment.MyChat.Enums;
    using MindLink.Recruitment.MyChat.Filters;
    using MindLink.Recruitment.MyChat.Interfaces;
    using Microsoft.Practices.Unity;
    using MindLink.Recruitment.MyChat;
    /// <summary>
    /// Represents a conversation exporter that can read a conversation and write it out in JSON.
    /// </summary>
    public sealed class ConversationExporter
    {
        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        static void Main(string[] args)
        {
            UnityContainer container = new UnityContainer();
            container.AddNewExtension<MyChatUnityContainer>();

            ICommandLineParser parser = container.Resolve<ICommandLineParser>();
            ConversationExporterConfiguration configuration = parser.ParseCommandLineArguments(args);

            IExporter export = container.Resolve<IExporter>();
            foreach (var argument in configuration.commandLineArguments)
            {
                if (argument.ArgumentType == CommandLineArgumentType.WordFilter)
                {
                    export.AddFilter(container.Resolve<IFilter>(argument.ArgumentType.ToString(), new ParameterOverride("replacement", "*redacted*")), argument.AdditionalParameters);
                }
                else
                {
                    export.AddFilter(container.Resolve<IFilter>(argument.ArgumentType.ToString()), argument.AdditionalParameters);
                }                
            }
            export.ExportConversation(configuration.inputFilePath, configuration.outputFilePath);
        }
    }
}
