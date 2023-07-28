using System;
using Combat;
using Combat.Projectiles;
using Entities;
using Entities.EnemyEntity;
using Moving;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Buildings
{
    public enum Side
    {
        Left,
        Up,
        Right,
        Down,
    }
    
    public class BuildingPart : MonoBehaviour, IEntity
    {
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private BoxCollider boxCollider;
        
        private Material _material;
        private Vector4 _neighbourIsDied = Vector4.zero;
        private Vector4 _defaultNeighbourIsDied = Vector4.zero;
        
        public EntityBoundary EntityBoundary { get; }
        public IMovingSystem MovingSystem { get; }
        public ICombatSystem CombatSystem { get; }
        public bool IsAlive { get; }

        public event Action OnDied;

        private void Awake()
        {
            _material ??= renderer.material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Enemy>())
            {
                Hit(null);
            }
            
        }

        public void SetNeighbour(Side side, BuildingPart neighbour)
        {
            Action action = side switch
            {
                Side.Left => () => _neighbourIsDied.x = 1,
                Side.Up => () => _neighbourIsDied.y = 1,
                Side.Right => () => _neighbourIsDied.z = 1,
                Side.Down => () => _neighbourIsDied.w = 1,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };

            if (neighbour == null)
            {
                SetDefaultNeighbour(side);
                _neighbourIsDied = _defaultNeighbourIsDied;
                UpdateNeighbours();
                return;
            }
            
            neighbour.OnDied += () =>
            {
                action.Invoke();
                UpdateNeighbours();
            };
        }

        private void SetDefaultNeighbour(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    _defaultNeighbourIsDied.x = 1;break;
                case Side.Up:
                    _defaultNeighbourIsDied.y = 1;break;
                case Side.Right:
                    _defaultNeighbourIsDied.z = 1;break;
                case Side.Down:
                    _defaultNeighbourIsDied.w = 1;break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public void Reset()
        {
            _material ??= renderer.material;
            boxCollider.enabled = true;
            _neighbourIsDied = _defaultNeighbourIsDied;
            _material.SetInt("_IsDamaged", 0);
            _material.SetVector("_Neightbours", _neighbourIsDied);
        }
        
        public bool Hit(Projectile projectile)
        {
            boxCollider.enabled = false;
            _material.SetVector("_NoiseUV", new Vector2(Random.Range(0, 10), Random.Range(0, 10)));
            _material.SetInt("_IsDamaged", 1);
            OnDied?.Invoke();
            return true;
        }

        private void UpdateNeighbours()
        {
            _material.SetVector("_Neightbours", _neighbourIsDied);
        }
        
        
    }
}