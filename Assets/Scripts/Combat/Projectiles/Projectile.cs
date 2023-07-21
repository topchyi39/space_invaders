using System;
using System.Linq;
using CameraTools;
using Entities;
using ObjectPolling;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using VisualEffects;
using Zenject;

namespace Combat.Projectiles
{
    public class Projectile : MonoBehaviour, IPoolObject
    {
        public class Factory : PlaceholderFactory<SoundManager, VisualEffectPool, Projectile> { }

        [SerializeField] private EntityBoundary cameraBoundary;
        [SerializeField] private BoxCollider hitBoundary;
        
        private SoundManager _soundManager;
        private VisualEffectPool _visualEffectPool;
        private Vector3 _direction;
        private float _speed;
        private bool _canMove;
        
        private Type[] _targetTypes;
        
        public Vector3 Direction => _direction;
        public float Speed => _speed;
        public bool CanMove => _canMove;
        public EntityBoundary CameraBoundary => cameraBoundary;
        public Vector3 ColliderSize => hitBoundary.size;
        
        public event Action Release;

        [Inject]
        private void Construct(SoundManager soundManager, VisualEffectPool visualEffectPool)
        {
            _soundManager = soundManager;
            _visualEffectPool = visualEffectPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IEntity otherEntity)) return;
            
            if (!_targetTypes.Contains(otherEntity.GetType())) return;

            if (otherEntity.Hit(this))
            {
                ReleaseProjectile(false);
            }
        }

        public Projectile Launch(Vector3 launchPosition, Vector3 direction, float speed)
        {
            
            _soundManager.PlaySound<ProjectileSound>(new ProjectileSoundData{ Type = ProjectileSoundType.Launch});
            transform.position = launchPosition;
            transform.forward = direction;
            _direction = direction;
            _speed = speed;
            _canMove = true;
            return this;
        }

        public Projectile UpdateTargets(params Type[] targetTypes)
        {
            _targetTypes = targetTypes;

            return this;
        }

        public void ReleaseProjectile(bool isOutOfCameraBoundary)
        {
            _canMove = false;
            Release?.Invoke();
            _soundManager.PlaySound<ProjectileSound>(new ProjectileSoundData{ Type = ProjectileSoundType.Hit});
            
            if (isOutOfCameraBoundary)
            {
                _visualEffectPool.SpawnEffect<DestroyingProjectileEffect>(transform.position, transform.rotation);
            }
        }


    }
    
    
    
}