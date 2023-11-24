// See https://aka.ms/new-console-template for more information
using ConsumerAndMailer;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Press enter to finish the program.");

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672/%2f");

using (var conn = factory.CreateConnection())
{
    using (var ch = conn.CreateModel())
    {
        var consumer = new EventingBasicConsumer(ch);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Inscription? insc = JsonConvert.DeserializeObject<Inscription>(message);
            if(insc == null)
            {
                Console.WriteLine("Unable to decode " + message);
            }
            else
            {
                Console.WriteLine($"You should send a mail to {insc.FullName} at {insc.MailAddress}");
            }
        };

        ch.BasicConsume(queue: "myQueue", autoAck: true, consumer: consumer);
        Console.ReadLine(); //This exists so the program doesn't end immediately
    }
}