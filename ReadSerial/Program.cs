using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadSerial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");

            var autoEvent = new AutoResetEvent(false);
            var serialToMqttConverter = new SerialToMqttConverter();
            var stateTimer = new Timer(serialToMqttConverter.Read,null,0, 250);

            Console.ReadKey();
            
        }

        public class SerialToMqttConverter
        {
            SerialPort serialArduino;
            public SerialToMqttConverter()
            {

                serialArduino = new SerialPort();
                serialArduino.PortName = "COM3";
                serialArduino.BaudRate = 115200;
                serialArduino.DtrEnable = true;
                serialArduino.Open();

                MqttClient client = new MqttClient("broker.hivemq.com");

            }

            public void Read(Object stateInfo)
            {
                var msg = serialArduino.ReadExisting();

                if (msg != string.Empty)
                {
                    Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff"));
                    Console.WriteLine(msg);
                }

            }
        }


    }
}
