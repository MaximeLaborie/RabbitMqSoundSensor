using Model;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Sensor
{
    public class MainViewModel : BindableBase
    {
        private const SensorNames SensorName = SensorNames.E;
        private const int SamplingInterval = 100;
        private const string ExchangeName = "Sounds";

        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";

        private ISoundSensor _sensor;
        private bool _closing = false;

        private IConnection _busConnection;
        private IModel _busChannel;

        public MainViewModel()
        {
            WindowClosingCommand = new DelegateCommand(OnWindowClosing);

            Data = new SensorData();
            Data.SensorName = SensorName;

            var factory = new ConnectionFactory() { HostName = HostName, UserName = UserName, Password = Password };
            _busConnection = factory.CreateConnection();
            _busChannel = _busConnection.CreateModel();
            _busChannel.ExchangeDeclare(exchange: ExchangeName, type: "fanout");

            _sensor = new SoundSensor();
            _sensor.PeakAcquired += OnPeak;
            _sensor.Start(SamplingInterval);
        }

        private void OnPeak(object sender, short peak)
        {
            Data.SoundLevel = peak;

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Data));
            if(!_closing)
                _busChannel.BasicPublish(exchange: ExchangeName, routingKey: "", basicProperties: null, body: body);
        }

        private void OnWindowClosing()
        {
            _closing = true;
            _busChannel.Close();
            _busConnection.Close();
        }

        #region properties

        private SensorData _data;

        public SensorData Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        #endregion properties

        #region commands

        public DelegateCommand WindowClosingCommand { get; set; }

        #endregion
    }
}