using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebPageParser.cs.ArgsParser;

namespace WebPageParser.cs.IoC
{
    public static class UnityConfiguration
    {
        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var unityContainer = new UnityContainer();
            RegisterTypes(unityContainer);
            return unityContainer;

        });

        public static IUnityContainer GetContainer() {

            return _container.Value;
        }

        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            //unityContainer.RegisterType<IArgumentsParser,ArgumentsParser>();
            //unityContainer.RegisterType<IArgumentsParser, ArgumentsParser>();


            var assembly = Assembly.GetAssembly(typeof(UnityConfiguration));
            var allClasses = AllClasses.FromAssemblies(assembly);

            unityContainer.RegisterTypes(
                allClasses,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }


    }
}
