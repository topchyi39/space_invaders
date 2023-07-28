using UnityEngine;
using Zenject;

namespace WalletComponents
{
    public class WalletInstaller : MonoInstaller
    {
        [SerializeField] private Wallet wallet;

        public override void InstallBindings()
        {
            Container.Bind<IWalletCollectable>().FromInstance(wallet).AsSingle().NonLazy();
            Container.Bind<IWallet>().FromInstance(wallet).AsSingle().NonLazy();
        }
    }
}