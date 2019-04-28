
namespace DDD.DomainLayer
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
