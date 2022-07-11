using Shopservices.RabbitMQ.Bus.Events;

namespace Shopservices.RabbitMQ.Bus.EventQueue
{
    public class EmailEventQueue : Event
    {
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Conent { get; set; }

         public EmailEventQueue(string receiver, string title, string conent)
        {
            this.Receiver = receiver;
            this.Title = title;
            this.Conent = conent;

        }

      
    }
}