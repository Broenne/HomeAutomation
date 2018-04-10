using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace ReadSerial
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");

            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Fake"))
                .AsImplementedInterfaces();
            var container = builder.Build();
            
            var serialToMqttConverter = container.Resolve<ISerialToMqttConverter>();
            var stateTimer = new Timer(serialToMqttConverter.Read,null,0, 250);

            Console.ReadKey();
            
        }


    }
}
