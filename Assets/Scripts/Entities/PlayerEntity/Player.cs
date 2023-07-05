using Moving;
using UnityEngine;

namespace Entities.PlayerEntity
{
    public class Player : MonoBehaviour, IEntity
    {
        public IMovingSystem MovingSystem { get; private set; }

        private void Awake()
        {
            MovingSystem = GetComponent<IMovingSystem>();
        }

    }
}
