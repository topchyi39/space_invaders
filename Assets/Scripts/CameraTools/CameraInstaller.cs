using UnityEngine;
using Zenject;

namespace CameraTools
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraBoundary cameraBoundary;


        public override void InstallBindings()
        {
            Container.Bind<CameraBoundary>().FromInstance(cameraBoundary).AsSingle();
        }
    }
}