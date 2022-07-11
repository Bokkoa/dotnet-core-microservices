using System.Threading.Tasks;
using Shopservices.RabbitMQ.Bus.Events;

namespace Shopservices.RabbitMQ.Bus.BusRabbit
{
    public interface IEventHandler<in TEvent>: IEventHandler where TEvent:Event
    {
         Task Handle(TEvent @event);

    }

    public interface IEventHandler{
        
    }
}