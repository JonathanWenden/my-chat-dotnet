using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MindLink.Recruitment.MyChat.Interfaces;
using MindLink.Recruitment.MyChat.Filters;
using MindLink.Recruitment.MyChat.Common;
using MindLink.Recruitment.MyChat.Exporters;
using MindLink.Recruitment.MyChat.Enums;

namespace MindLink.Recruitment.MyChat
{
    public class MyChatUnityContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddExtension(new MyChatCommonUnityContainer())
                .RegisterType<IExporter, Exporter>()
                .RegisterType<ICommandLineParser, CommandLineArgumentParser>()
                .RegisterType<IFilter, UserFilter>(CommandLineArgumentType.UserFilter.ToString())
                .RegisterType<IFilter, WordFilter>(CommandLineArgumentType.WordFilter.ToString())
                .RegisterType<IFilter, MessageFilter>(CommandLineArgumentType.MessageFilter.ToString());
        }
    }
}
