using UnityEngine;
using Zenject;

namespace OriginContainer
{
    public class OriginInstaller : MonoInstaller
    {
        [SerializeField] private Origin mainOrigin;

        public override void InstallBindings()
        {
            Container.Bind<IOrigin>().To<Origin>().FromInstance(mainOrigin);
        }
    }
}