using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPageParser.cs.IoC;

namespace WebPageParser.cs
{
    public static class Program
    {
        private static IUnityContainer _container;
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Web page parser started");
            Bootstrap();
            Console.WriteLine("Starting application");
            var launcher = _container.Resolve<Launcher>();
            launcher.Execute(args);
            Console.WriteLine("Aplication finished");
        }


        private static void Bootstrap()
        {
            _container = UnityConfiguration.GetContainer();
        }
    }
}
