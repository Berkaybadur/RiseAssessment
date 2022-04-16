using NewsApp.RabbitMQ.Api.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using NewsApp.RabbitMQ.Api.Models;
using System.Text;

namespace NewsApp.RabbitMQ.Api
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendToQueue(Contact contact)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "ProjeRabbit", Password = "TEST123" };//Konfigurasyondan alınabilir            
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ContactQueue",
                    durable: false, //Data saklama yöntemi (memory-fiziksel)
                    exclusive: false, //Başka bağlantıların kuyruğa ulaşmasını istersek true kullanabiliriz.
                    autoDelete: false,
                    arguments: null);//Exchange parametre tanımları.          

                var body = Encoding.UTF8.GetBytes(contact.Name);
                channel.BasicPublish(exchange: "",
                    routingKey: "ContactRoute", basicProperties: null, body: body);
            }
        }
    }
}
