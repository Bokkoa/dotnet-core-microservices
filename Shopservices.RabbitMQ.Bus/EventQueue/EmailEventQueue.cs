using Shopservices.RabbitMQ.Bus.Events;

namespace Shopservices.RabbitMQ.Bus.EventQueue
{
    public class EmailEventQueue : Event
    {
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

         public EmailEventQueue(string receiver, string title, string content)
        {
            this.Receiver = receiver;
            this.Title = title;
            this.Content = content;

        }

      
    }
}