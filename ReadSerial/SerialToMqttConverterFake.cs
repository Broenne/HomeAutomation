using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ReadSerial
{
        public class SerialToMqttConverterFake : ISerialToMqttConverter
        {
            private MqttClient client;
            public SerialToMqttConverterFake()
            {
                client = new MqttClient("broker.hivemq.com");
                byte code = client.Connect(Guid.NewGuid().ToString(), "username", "password");
            }


            private static Random rnd = new Random();

            public void Read(object stateInfo)
            {
                try
                {
                    var temp = rnd.Next(1020, 1040) / 100.0;
                    Console.WriteLine(temp);

                    if (client != null)
                    {
                        ushort msgId = client.Publish("/my_topic", // topic
                            Encoding.UTF8.GetBytes($"temp:{temp}"), // message body
                            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                            false); // retained
                    }

                // evtl reconnect
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            
            }
    }
}
