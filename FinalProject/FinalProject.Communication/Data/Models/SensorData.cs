using FinalProject.Communication.Data.Enums;

namespace FinalProject.Communication.Data.Models
{
    public class SensorData
    {
        public SensorData(Characteristics characteristic, byte[] data)
        {
            Characteristic = characteristic;
            RawData = data;
        }

        public Characteristics Characteristic { get; set; }

        public byte[] RawData { get; set; }

        public int Value => RawData.FirstOrDefault();
    }
}
