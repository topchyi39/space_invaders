using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Skybox
{
    public class SkyboxInstaller : MonoInstaller
    {
        [SerializeField] private SkyboxSettings skyboxSettings;

        public override void InstallBindings()
        {
            Container.Bind<SkyboxSettings>().FromInstance(skyboxSettings);
        }
    }
}