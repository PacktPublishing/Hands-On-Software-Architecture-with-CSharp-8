using System.Threading.Tasks;

namespace DDD.ApplicationLayer
{
    public interface ICommandHandler<T>
        where T : ICommand
    {
        Task HandleAsync(T command);
    }
    
}
