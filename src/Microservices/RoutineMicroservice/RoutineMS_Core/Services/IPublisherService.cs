using System.Threading.Tasks;

namespace RoutineMS_Core.Services
{
    public interface IPublisherService
    {
        void PublishEvent(object data, string exchange, string routingKey);
    }
}
