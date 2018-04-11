using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ReadSerial.Client
{
    /// <summary>
    /// The MQTT console client
    /// </summary>
    /// <seealso cref="ReadSerial.Client.IMqttConsoleClient" />
    public class MqttConsoleClient : IMqttConsoleClient
    {
        private MqttClient client;
       
        private void ClientOnMqttMsgPublishReceived(
            object sender, 
            MqttMsgPublishEventArgs mqttMsgPublishEventArgs)
        {
            var msgId = System.Text.Encoding.Default.GetString(mqttMsgPublishEventArgs.Message);

            Console.WriteLine($"Client: {msgId}");
        }

        public void Start()
        {

            try
            {
                client = new MqttClient("broker.hivemq.com");

                client.MqttMsgPublishReceived += ClientOnMqttMsgPublishReceived;
                byte code = this.client.Connect(Guid.NewGuid().ToString(),
                    "username", "password");
                ushort msgId = client.Subscribe(new string[] { "/my_topic" },
                    new byte[]
                    {
                    //    MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                        MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
