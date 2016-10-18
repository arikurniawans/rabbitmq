using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace latrbjs
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonService commonService = new CommonService();
            IConnection connection = commonService.GetRabbitMqConnection();
            IModel model = connection.CreateModel();
            SetupSerialisationMessageQueue(model);
            RunSerialisationDemo(model);
        }

        private static void SetupSerialisationMessageQueue(IModel model)
        {
            model.QueueDeclare(CommonService.SerialisationQueueName, true, false, false, null);
        }

        private static void RunSerialisationDemo(IModel model)
        {
            Console.WriteLine("Enter customer name. Quit with 'q'.");
            while (true)
            {
                string customerName = Console.ReadLine();
                if (customerName.ToLower() == "q") break;
                Customer customer = new Customer() { Name = customerName };
                IBasicProperties basicProperties = model.CreateBasicProperties();
                basicProperties.SetPersistent(true);
                String jsonified = JsonConvert.SerializeObject(customer);
                byte[] customerBuffer = Encoding.UTF8.GetBytes(jsonified);
                model.BasicPublish("", CommonService.SerialisationQueueName, basicProperties, customerBuffer);
            }
        }
    }
}
