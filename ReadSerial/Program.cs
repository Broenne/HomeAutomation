using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ReadSerial.Client;

namespace ReadSerial
{
    using System.Runtime.CompilerServices;

    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");

            var container = CreateContainer();

            var serialToMqttConverter = container.Resolve<ISerialToMqttConverter>();
            var stateTimer = new Timer(serialToMqttConverter.Read, null, 0, 250);
            var client = container.Resolve<IMqttConsoleClient>();
            client.Start();
            Console.ReadKey();
            
        }



        private static IContainer CreateContainer()
        {

            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Fake"))
                .AsImplementedInterfaces();

            builder.RegisterType<MqttConsoleClient>().As<IMqttConsoleClient>();
            return builder.Build();
        }

    }
}
