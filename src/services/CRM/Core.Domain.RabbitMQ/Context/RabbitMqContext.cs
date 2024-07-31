//using Core.Domain.RabbitMq.Options;
//using System.Text;

//namespace Core.Domain.RabbitMQ.Context
//{
//    public class RabbitMqContext
//    {
//        private readonly string _hostname;
//        private readonly string _password;
//        private readonly string _queueName;
//        private readonly string _username;
//        private readonly int _port;
//        private IConnection _connection;

//        public RabbitMqContext(IOptions<RabbitMqConfiguration> rabbitMqOptions)
//        {
//            _queueName = rabbitMqOptions.Value.QueueName;
//            _hostname = rabbitMqOptions.Value.Hostname;
//            _username = rabbitMqOptions.Value.UserName;
//            _password = rabbitMqOptions.Value.Password;
//            _port = rabbitMqOptions.Value.Port;

//            CreateConnection();
//        }

//        public void Send<T>(T obj)
//        {
//            if (ConnectionExists())
//            {
//                using (var channel = _connection.CreateModel())
//                {
//                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

//                    var json = JsonConvert.SerializeObject(obj);
//                    var body = Encoding.UTF8.GetBytes(json);

//                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
//                }
//            }
//        }

//        private void CreateConnection()
//        {
//            try
//            {
//                var factory = new ConnectionFactory
//                {
//                    HostName = _hostname,
//                    UserName = _username,
//                    Password = _password,
//                    Port = _port
//                };
//                _connection = factory.CreateConnection();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Could not create connection: {ex.Message}");
//            }
//        }

//        private bool ConnectionExists()
//        {
//            if (_connection != null)
//            {
//                return true;
//            }

//            CreateConnection();

//            return _connection != null;
//        }
//    }
//}
