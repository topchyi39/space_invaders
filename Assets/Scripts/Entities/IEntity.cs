﻿using System;
using Combat;
using Combat.Projectiles;
using Moving;

namespace Entities
{
    public interface IEntity
    {
        EntityBoundary EntityBoundary { get; }
        IMovingSystem MovingSystem { get; }
        ICombatSystem CombatSystem { get; }
        public bool IsAlive { get; }
        event Action OnDied; 
        bool Hit(Projectile projectile);
    }
}