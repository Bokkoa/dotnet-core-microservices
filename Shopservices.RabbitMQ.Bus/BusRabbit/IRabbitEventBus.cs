using System.Threading.Tasks;
using Shopservices.RabbitMQ.Bus.Commands;
using Shopservices.RabbitMQ.Bus.Events;

namespace Shopservices.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
         
         Task SendCommand<T>(T command ) where T:Command;

         void Publish<T>(T @event ) where T: Event;

         void Subscribe<T, TH>() where T: Event
                                 where TH: IEventHandler<T>;

    }
}