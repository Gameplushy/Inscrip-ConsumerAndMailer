// See https://aka.ms/new-console-template for more information
using ConsumerAndMailer;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Mail;
using System.Text;

Console.WriteLine("Press enter to finish the program.");
try
{
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
                if (insc == null)
                {
                    Console.WriteLine("Unable to decode " + message);
                }
                else
                {
                    Console.WriteLine($"Sending a mail to {insc.FullName} at {insc.MailAddress}");
                    SendMail(insc);
                }
            };

            ch.BasicConsume(queue: "myQueue", autoAck: true, consumer: consumer);
            Console.ReadLine(); //This exists so the program doesn't end immediately
        }
    }
}
catch (BrokerUnreachableException)
{
    Console.WriteLine("Rabbit server broker was not found. Did you forget to turn on its Docker image?");
}

void SendMail(Inscription insc)
{
    MailMessage message = new MailMessage("inscrip.examp@fakemail.com", insc.MailAddress)
    {
        Subject = "You're signed up!",
        Body = $"Hello {insc.FullName}! You are now signed up to our database.",
        IsBodyHtml = false
    };

    using (var smtpClient = new SmtpClient("localhost", 1025))
    {
        smtpClient.EnableSsl = false;
        try
        {
            smtpClient.Send(message);
            Console.WriteLine($"Message successfully sent to {insc.MailAddress}!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Message failed... {e.Message}\n{e.StackTrace}");
        }
    }
}