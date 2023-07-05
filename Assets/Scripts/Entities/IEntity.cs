using Moving;

namespace Entities
{
    public interface IEntity
    {
        IMovingSystem MovingSystem { get; }
    }
}