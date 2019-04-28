using System.Threading.Tasks;
using DDD.DomainLayer;

namespace DDD.ApplicationLayer
{
    public interface IEventHandler<T>
    where T : IEventNotification
    {
        Task HandleAsync(T ev);
    }
}
