using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindLink.Recruitment.MyChat.Exporters;
namespace MindLink.Recruitment.MyChat.Interfaces
{
    public interface ICommandLineParser
    {
        ConversationExporterConfiguration ParseCommandLineArguments(string[] arguments);
    }
}
