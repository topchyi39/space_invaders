using UnityEngine;
using Zenject;

namespace Sounds
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private SoundManager soundManager;

        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(soundManager).AsSingle();
        }
    }
}