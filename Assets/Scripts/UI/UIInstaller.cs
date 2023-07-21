using UnityEngine;
using Zenject;

namespace UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIManager uiManager;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        }
    }
}