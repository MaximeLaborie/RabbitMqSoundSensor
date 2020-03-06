using Prism.Mvvm;

namespace Model
{
    public class SensorData : BindableBase
    {
        private SensorNames? _sensorName;

        public SensorNames? SensorName
        {
            get { return _sensorName; }
            set { SetProperty(ref _sensorName, value); }
        }

        private short _soundLevel;

        public short SoundLevel
        {
            get { return _soundLevel; }
            set { SetProperty(ref _soundLevel, value); }
        }
    }
}