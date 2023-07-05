using UnityEngine;
using Zenject;

namespace Input
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput playerInput;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerInput>().FromInstance(playerInput).AsSingle();
        }
    }
}