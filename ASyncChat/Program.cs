using System;
using ChatLogLib;
//using Logging;
using ChatLib;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using Ninject;


namespace ASyncChat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Step 2
            //Application.Run(new Form1(new Client(new NLog())));
            //Step 3
            /// <summary>
            /// UNITY CONTAINER
            /// </summary>
            UnityContainer container = new UnityContainer();
            //container.RegisterType<ILoggingService, TextLogger>();
            //container.RegisterType<ILoggingService, NLog>();
            container.RegisterType<ChatLogLib.ILoggingService, ChatLogLib.ChatLogger>();
            Application.Run(container.Resolve<Form1>());

            ///<summary>
            /// Ninject Container
            /// </summary>
            //IKernel kernel = new StandardKernel();
            ////kernel.Bind<ILoggingService>().To<TextLogger>();
            //kernel.Bind<ChatLogLib.ILoggingService>().To<ChatLogLib.ChatLogger>();
            //Application.Run(kernel.Get<Form1>());

            //Run the app



        }
    }
}
