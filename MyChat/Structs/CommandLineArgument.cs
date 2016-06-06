using System.Collections.Generic;
using MindLink.Recruitment.MyChat.Enums;
namespace MindLink.Recruitment.MyChat.Structs
{
    public struct CommandLineArgument
    {
        public CommandLineArgumentType ArgumentType;
        public List<string> AdditionalParameters;
    }
}