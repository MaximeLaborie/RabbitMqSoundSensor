using Model;
using Newtonsoft.Json;
using Prism.Mvvm;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Map
{
    public class MapViewModel : BindableBase
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string ExchangeName = "Sounds";

        private IConnection _busConnection;
        private IModel _busChannel;

        public MapViewModel()
        {
            var factory = new ConnectionFactory() { HostName = HostName, UserName = UserName, Password = Password };
            _busConnection = factory.CreateConnection();
            _busChannel = _busConnection.CreateModel();
            _busChannel.ExchangeDeclare(exchange: ExchangeName, type: "fanout");
            var queueResult = _busChannel.QueueDeclare(queue: "", exclusive: true);
            _busChannel.QueueBind(queueResult.QueueName, ExchangeName, "");

            var consumer = new EventingBasicConsumer(_busChannel);
            consumer.Received += OnMessage;
            _busChannel.BasicConsume(queue: queueResult.QueueName, autoAck: true, consumer: consumer);
        }

        private void OnMessage(object sender, BasicDeliverEventArgs e)
        {
            var data = JsonConvert.DeserializeObject<SensorData>(Encoding.UTF8.GetString(e.Body));
            var existingData = Sensors.FirstOrDefault(s => s.SensorName == data.SensorName);

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                if (existingData != null)
                {
                    existingData.SoundLevel = data.SoundLevel;
                }
                else
                {
                    Sensors.Add(data);
                }
            });
        }

        private ObservableCollection<SensorData> _sensors = new ObservableCollection<SensorData>();

        public ObservableCollection<SensorData> Sensors
        {
            get { return _sensors; }
            set { SetProperty(ref _sensors, value); }
        }
    }
}