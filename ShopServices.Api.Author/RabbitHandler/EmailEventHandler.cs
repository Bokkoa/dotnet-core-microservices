using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shopservices.RabbitMQ.Bus.BusRabbit;
using Shopservices.RabbitMQ.Bus.EventQueue;
using ShopServices.Messenger.Email.SendGridLibrary.Interface;
using ShopServices.Messenger.Email.SendGridLibrary.Model;

namespace ShopServices.Api.Author.RabbitHandler
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        
        private readonly ILogger<EmailEventHandler> _logger;
        private readonly ISendGridSend _sendGridSend;
        private readonly IConfiguration _config;
        public EmailEventHandler(ILogger<EmailEventHandler> logger, ISendGridSend sendGridSend, IConfiguration config)
        {
            _logger = logger;
            _sendGridSend = sendGridSend;
            _config = config;
        }


        public async Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation(@event.Title);

            var objData = new SendGridData();
            objData.Content = @event.Content;
            objData.Title = @event.Title;
            objData.EmailReceiver = @event.Receiver;
            objData.NameReceiver = @event.Receiver;
            objData.NameReceiver = @event.Receiver;
            objData.SendGridApiKey = _config["SendGrid:ApiKey"];
            
            var result = await _sendGridSend.SendMail(objData);

            // SEND RABBIT MESSAGE IF EMAIL SENDED
            if(result.result){
                await Task.CompletedTask;
                return;
            }

        }
    }
}