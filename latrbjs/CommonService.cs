using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace latrbjs
{
    class CommonService
    {
        private string _hostName = "167.205.66.183";
        private string _userName = "gue";
        private string _password = "gue";

        public static string SerialisationQueueName = "lumen.json.lagi";

        public IConnection GetRabbitMqConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = _hostName;
            connectionFactory.UserName = _userName;
            connectionFactory.Password = _password;

            return connectionFactory.CreateConnection();
        }
    }
}
