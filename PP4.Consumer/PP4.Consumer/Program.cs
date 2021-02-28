﻿using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PP4.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rpc_queue", durable: false,
                    exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "rpc_queue",
                    autoAck: false, consumer: consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var body = ea.Body.ToArray();
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        var serializedMessage = Encoding.UTF8.GetString(body);
                        var messagePm = JsonConvert.DeserializeObject<ProcessedMessagePm>(serializedMessage);
                        using var context = new ProcessedMessageContext();
                        {
                            context.Messages.Add(messagePm);
                            context.SaveChanges();
                        }
                        Console.WriteLine($"Saved message with an Id: {messagePm.Id}");
                        var messageResponse = new MessageResponseModel
                        {
                            ExternalId = messagePm.Id,
                            Message = messagePm.Message
                        };
                        response = JsonConvert.SerializeObject(messageResponse);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error processing the message:" + e.Message);
                        response = "";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                            basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag,
                            multiple: false);
                    }
                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
