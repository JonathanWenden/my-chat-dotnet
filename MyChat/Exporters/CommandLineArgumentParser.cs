using System.Collections.Generic;
using MindLink.Recruitment.MyChat.Structs;
using MindLink.Recruitment.MyChat.Enums;
using MindLink.Recruitment.MyChat.Interfaces;
namespace MindLink.Recruitment.MyChat.Exporters
{
    /// <summary>
    /// Represents a helper to parse command line arguments.
    /// </summary>
    public sealed class CommandLineArgumentParser : ICommandLineParser
    {

        /// <summary>
        /// Parses the given <paramref name="arguments"/> into the exporter configuration.
        /// </summary>
        /// <param name="arguments">
        /// The command line arguments.
        /// </param>
        /// <returns>
        /// A <see cref="ConversationExporterConfiguration"/> representing the command line arguments.
        /// </returns>
        public ConversationExporterConfiguration ParseCommandLineArguments(string[] arguments)
        {
            if (arguments.Length >= 2)
            {
                ConversationExporterConfiguration config =  new ConversationExporterConfiguration
                    {
                        inputFilePath = arguments[0],
                        outputFilePath = arguments[1],
                    };
                if (arguments.Length > 2)
                {
                    config.commandLineArguments = ReadOptionalCommandLineArguments(arguments);
                }
                else
                {
                    config.commandLineArguments = new List<CommandLineArgument>();
                }
                
                return config;
            }
            else
            {
                return new ConversationExporterConfiguration
                {
                    inputFilePath = "",
                    outputFilePath = "",
                    commandLineArguments = new List<CommandLineArgument>()
                };
            }
       }

        /// <summary>
        /// Converts an array of command line arguments into a list of command line arguments
        /// </summary>
        /// <param name="arguments">The command line arguments</param>
        /// <returns>A List of command line argument structs read converted from the command line arguments</returns>
        private List<CommandLineArgument> ReadOptionalCommandLineArguments(string[] arguments)
        {
            List<CommandLineArgument> commandLineArguments = new List<CommandLineArgument>();

            string argument = "";
            List<string> parameters = new List<string>();
            for (int i = 2; i < arguments.Length; i++)
            {
                if (arguments[i].Contains("-"))
                {
                    if (argument != "")
                    {
                        commandLineArguments.Add(CreateCommandLineArgument(argument, parameters));
                        argument = arguments[i];
                        parameters.Clear();
                    }
                    else
                    {
                        argument = arguments[i];
                    }
                }
                else
                {
                    parameters.Add(arguments[i]);
                }
            }
            if (argument != "")
            {
                commandLineArguments.Add(CreateCommandLineArgument(argument, parameters));
            }

            return commandLineArguments;
        }
        /// <summary>
        /// Creates a single command line argument based on a type and list of parameters
        /// </summary>
        /// <param name="type">The type of command</param>
        /// <param name="parameters">The parameters from the command line</param>
        /// <returns>A command line argument</returns>
        private CommandLineArgument CreateCommandLineArgument(string type, List<string> parameters)
        {
            return new CommandLineArgument
            {
                ArgumentType = SelectCommandLineType(type),
                AdditionalParameters = new List<string>(parameters)
            };
        }
        
        /// <summary>
        /// Converts a string representation of the type into a CommandLineArgumentType enum
        /// </summary>
        /// <param name="type">An string representation of the type</param>
        /// <returns>A commandLine arguementType array</returns>
        private CommandLineArgumentType SelectCommandLineType(string type)
        {
            switch (type)
            {
                case "-U":
                    return CommandLineArgumentType.UserFilter;
                case "-M":
                    return CommandLineArgumentType.MessageFilter;
                case "-W":
                    return CommandLineArgumentType.WordFilter;
                default:
                    return CommandLineArgumentType.None;
            }
        }
    }
}
