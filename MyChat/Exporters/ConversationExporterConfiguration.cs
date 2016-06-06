using System.Collections.Generic;
using MindLink.Recruitment.MyChat.Structs;

namespace MindLink.Recruitment.MyChat.Exporters
{
    using System;

    /// <summary>
    /// Represents the configuration for the exporter.
    /// </summary>
    public struct ConversationExporterConfiguration
    {
        public string inputFilePath;
        public string outputFilePath;

        public List<CommandLineArgument> commandLineArguments;
    }
}
