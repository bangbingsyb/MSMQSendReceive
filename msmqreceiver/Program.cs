using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace msmqreceiver
{
    class Program
    {
        public class MSMQReceiver
        {
            public static void Main()
            {
                MSMQReceiver msmqReceiver = new MSMQReceiver();
                Console.WriteLine("** Please enter the queue path:");
                Console.WriteLine(@"Example private queue path: ComputerName\PRIVATE$\QueueName  ");
                Console.WriteLine(@"Example public queue path: ComputerName\QueueName ");
                string queuePath = Console.ReadLine();

                try
                {
                    if (!MessageQueue.Exists(queuePath))
                    {
                        MessageQueue.Create(queuePath);
                        Console.WriteLine("Queue created: " + queuePath);
                    }

                    using (MessageQueue messageQueue = new MessageQueue(queuePath))
                    {
                        while (true)
                        {
                            Console.WriteLine();
                            Message msg = messageQueue.Receive();
                            msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[1] { typeof(string) });
                            Console.WriteLine("Message received: " + msg.Body);
                        }
                    }
                }
                catch (MessageQueueException)
                {
                    Console.WriteLine("Message queue internal error.");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadKey();
            }
        }
    }
}

