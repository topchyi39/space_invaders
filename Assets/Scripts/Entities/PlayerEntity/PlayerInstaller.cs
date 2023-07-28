using GameComponents;
using UI.Screens;
using UnityEngine;
using Visual.PlayerVisual;
using Visual.PlayerVisualComponents;
using Zenject;

namespace Entities.PlayerEntity
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform gameObjectsParent;
        [SerializeField] private PlayerSelector playerSelector;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerSelector>().FromInstance(playerSelector);
            Container.BindFactory<Game, PlayerSelector, Player, Player.Factory>().FromComponentInNewPrefab(playerPrefab);
        }
    }
}