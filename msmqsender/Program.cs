using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace msmqsender
{
    class Program
    {
        public class MSMQSender
        {

            public static void Main()
            {
                MSMQSender msmqSender = new MSMQSender();

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
                            Console.Write("** Please enter the message: ");

                            string msg = Console.ReadLine();
                            messageQueue.Send(msg);
                            Console.WriteLine("Sent message to queue " + queuePath + ": " + msg);
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
