using System;
using Shopservices.RabbitMQ.Bus.Events;

namespace Shopservices.RabbitMQ.Bus.Commands
{
    public class Command: Message
    {
        public DateTime Timestamp { get; protected set; }
        protected Command(){
            Timestamp = DateTime.Now;
        }
    }
}