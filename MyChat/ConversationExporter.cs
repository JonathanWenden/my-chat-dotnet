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
            var conversationExporter = new ConversationExporter();
            ConversationExporterConfiguration configuration = new CommandLineArgumentParser().ParseCommandLineArguments(args);

            Exporter export = new Exporter();
            foreach (var argument in configuration.commandLineArguments)
            {
                switch (argument.ArgumentType)
                {
                    case CommandLineArgumentType.UserFilter:
                        export.AddFilter(new UserFilter(), argument.AdditionalParameters);
                        break;
                    case CommandLineArgumentType.MessageFilter:
                        export.AddFilter(new MessageFilter(), argument.AdditionalParameters);
                        break;
                    case CommandLineArgumentType.WordFilter:
                        export.AddFilter(new WordFilter("*redacted*"), argument.AdditionalParameters);
                        break;
                }
            }
            export.ExportConversation(configuration.inputFilePath, configuration.outputFilePath);
        }
    }
}
