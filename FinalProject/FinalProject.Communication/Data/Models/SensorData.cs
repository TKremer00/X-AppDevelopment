using FinalProject.Communication.Data.Enums;

namespace FinalProject.Communication.Data.Models
{
    public class SensorData
    {
        private readonly Func<byte[], int> _calcRealValue;

        public SensorData(Characteristics characteristic, byte[] data)
        {
            Characteristic = characteristic;
            RawData = data;
            _calcRealValue = (value) => value.FirstOrDefault();
        }

        public SensorData(Characteristics characteristic, byte[] data, Func<byte[], int> calcRealValue)
        {
            Characteristic = characteristic;
            RawData = data;
            _calcRealValue = calcRealValue;

        }

        public Characteristics Characteristic { get; set; }

        public byte[] RawData { get; set; }

        public int Value => _calcRealValue.Invoke(RawData);

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LocalTime => CreatedAt.ToLocalTime();
    }
}
