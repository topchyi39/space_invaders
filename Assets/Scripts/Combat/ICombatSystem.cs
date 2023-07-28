
using Entities;

namespace Combat
{
    public interface ICombatSystem
    {
        void Enable();
        void Disable();
        void SetEntityBoundary(EntityBoundary entityBoundary);
    }
}