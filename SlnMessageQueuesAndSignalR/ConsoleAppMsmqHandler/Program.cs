using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleAppMsmqHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var queue = new MessageQueue(@".\private$\TestQueue"))
            {
                while (true)
                {
                    Console.WriteLine("Listening TestQueue");

                    var message = queue.Receive();

                    var bodyReader = new StreamReader(message.BodyStream);

                    var xmlBody = bodyReader.ReadToEnd();

                    Console.WriteLine("Message Received: {0}", xmlBody);
                }
            }
        }
    }
}
