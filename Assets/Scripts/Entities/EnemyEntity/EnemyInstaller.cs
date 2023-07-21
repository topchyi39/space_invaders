using Combat;
using ObjectPolling;
using UnityEngine;
using Zenject;

namespace Entities.EnemyEntity
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy enemyPrefab;
        
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private EnemiesContainer enemiesContainer;
        
        public override void InstallBindings()
        {
            Container.BindFactory<EnemyParams, VisualEffectPool, Enemy, Enemy.Factory>()
                     .FromComponentInNewPrefab(enemyPrefab);
            
            Container.Bind<EnemySpawner>().FromInstance(enemySpawner).AsSingle();
            Container.Bind<EnemiesContainer>().FromInstance(enemiesContainer).AsSingle();
        }
    }
}