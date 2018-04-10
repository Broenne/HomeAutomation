namespace ReadSerial
{
    public interface ISerialToMqttConverter
    {
        void Read(object stateInfo);
    }
}