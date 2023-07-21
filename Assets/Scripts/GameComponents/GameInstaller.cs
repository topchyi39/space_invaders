using UnityEngine;
using Zenject;

namespace GameComponents
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Game game;

        public override void InstallBindings()
        {
            Container.Bind<Game>().FromInstance(game).AsSingle();
        }
    }
}