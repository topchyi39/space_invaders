using UnityEngine;
using Zenject;

namespace Ads
{
    public class AdsInstaller : MonoInstaller
    {
        [SerializeField] private MobileAds ads;
        [SerializeField] private GoogleAdsResolver resolver;
        

        public override void InstallBindings()
        {
            Container.Bind<IAdsResolver>().FromInstance(resolver);
            Container.Bind<MobileAds>().FromInstance(ads);
        }
    }
}