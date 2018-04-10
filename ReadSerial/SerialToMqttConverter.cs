using System;
using System.IO.Ports;
using uPLibrary.Networking.M2Mqtt;

namespace ReadSerial
{
        public class SerialToMqttConverter : ISerialToMqttConverter
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
