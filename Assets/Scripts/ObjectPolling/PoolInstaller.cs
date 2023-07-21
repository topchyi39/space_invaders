using CameraTools;
using Combat.Projectiles;
using Sounds;
using UnityEngine;
using Zenject;

namespace ObjectPolling
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilePool projectilePool;
        [SerializeField] private VisualEffectPool visualEffectPool;
        [SerializeField] private SoundEffectPool soundEffectPool;
        
        public override void InstallBindings()
        {
            Container.BindFactory<SoundManager, VisualEffectPool, Projectile, Projectile.Factory>()
                     .FromComponentInNewPrefab(projectilePool.Prefab);
            
            Container.Bind<ProjectilePool>().FromInstance(projectilePool).AsSingle();
            Container.Bind<VisualEffectPool>().FromInstance(visualEffectPool).AsSingle();
            Container.Bind<SoundEffectPool>().FromInstance(soundEffectPool).AsSingle();
        }
    }
}