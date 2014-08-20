using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleAppMsmqStudy
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Message Queue Tester");

            using (var queue = new MessageQueue(@".\private$\TestQueue"))
            {

                queue.Send("Message: Hello World 2");

            }

            Console.WriteLine("First Message sended");

            var newMessage = new NewMessage
            {
                Message = "Mensagem de teste 2"
            };

            if (MessageQueue.Exists(@".\private$\TestTransactionalQueue"))
            {
                var queue = new MessageQueue(@".\private$\TestTransactionalQueue", true);


                using (queue)
                {
                    var tx = new MessageQueueTransaction();

                    var message = new Message();

                    var xmlSerializer = new XmlSerializer(typeof (NewMessage));

                    var newMessageStream = new MemoryStream();

                    xmlSerializer.Serialize(newMessageStream, newMessage);

                    message.BodyStream = newMessageStream;

                    tx.Begin();
                    queue.Send(message, tx);
                    tx.Commit();
                }
            }
            Console.WriteLine("Second Message sended with transaction");
        }
    }

    public class NewMessage
    {
        public string Message { get; set; }
    }
}
