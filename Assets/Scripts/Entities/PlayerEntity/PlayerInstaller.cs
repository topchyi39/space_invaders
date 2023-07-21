using GameComponents;
using UnityEngine;
using Zenject;

namespace Entities.PlayerEntity
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform gameObjectsParent;
        
        public override void InstallBindings()
        {
            Container.BindFactory<Game, Player, Player.Factory>().FromComponentInNewPrefab(playerPrefab);
        }
    }
}