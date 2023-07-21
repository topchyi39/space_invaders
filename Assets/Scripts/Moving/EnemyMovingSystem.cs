using Entities;
using UnityEngine;
using Visual;

namespace Moving
{
    public class EnemyMovingSystem : MonoBehaviour, IMovingSystem
    {
        private EnemyVisual _enemyVisual;

        private bool _enabled;

        public Vector3 Position => transform.position;
        
        public void SetEntityBoundary(EntityBoundary boundary) { }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void MoveManually(Vector3 position)
        {
            if (!_enabled) return;
            
            if(_enemyVisual) _enemyVisual.ToggleVisual();
            
            transform.position = position;
        }

        public void SetEnemyVisual(EnemyVisual visual)
        {
            _enemyVisual = visual;
        }
    }
}