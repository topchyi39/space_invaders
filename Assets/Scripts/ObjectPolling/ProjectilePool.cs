using CameraTools;
using Combat.Projectiles;
using GameComponents;
using Sounds;
using UnityEngine;
using Zenject;

namespace ObjectPolling
{
    
    
    public class ProjectilePool : AbstractObjectPool<Projectile>, IGameDisposable
    {
        [SerializeField] private ProjectileMoving projectileMoving;

        private SoundManager _soundManager;
        private VisualEffectPool _visualEffectPool;
        private Projectile.Factory _projectileFactory;
        
        [Inject]
        private void Construct(SoundManager soundManager, VisualEffectPool visualEffectPool, Projectile.Factory projectileFactory, Game game)
        {
            _soundManager = soundManager;
            _projectileFactory = projectileFactory;
            _visualEffectPool = visualEffectPool;
        }
        
        protected override Projectile CreatePoolItem()
        {
            var projectile = _projectileFactory.Create(_soundManager, _visualEffectPool);

            projectile.transform.SetParent(transform);
            projectile.gameObject.SetActive(false);
            
            projectile.Release += () => Pool.Release(projectile);

            return projectile;
        }
        
        public void OnGameDispose()
        {
            Pool.Clear();
        }

        protected override void OnReleaseObjectCallBack(Projectile poolObject) { }

        protected override void OnGetObjectCallBack(Projectile poolObject) { }

        protected override void OnDestroyPoolObjectCallBack(Projectile poolObject) { }
    }
}