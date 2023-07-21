using Entities;
using UnityEngine;

namespace Moving
{
    public interface IMovingSystem
    {
        Vector3 Position { get; }
        
        void SetEntityBoundary(EntityBoundary boundary);
        void Enable();
        void Disable();

        virtual void MoveManually(Vector3 position) { }
    }
}