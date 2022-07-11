using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shopservices.RabbitMQ.Bus.BusRabbit;
using Shopservices.RabbitMQ.Bus.EventQueue;

namespace ShopServices.Api.Author.RabbitHandler
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        
        private readonly ILogger<EmailEventHandler> _logger;
        public EmailEventHandler(ILogger<EmailEventHandler> logger)
        {
            _logger = logger;
        }


        public Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation(@event.Title);
            return Task.CompletedTask;
        }
    }
}