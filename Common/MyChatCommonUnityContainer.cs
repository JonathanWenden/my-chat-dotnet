using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MindLink.Recruitment.MyChat.Common.Interfaces;

namespace MindLink.Recruitment.MyChat.Common
{
    public class MyChatCommonUnityContainer : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ILogger, Logger>();
        }
    }
}
