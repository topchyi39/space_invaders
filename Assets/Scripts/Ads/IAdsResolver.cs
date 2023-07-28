using System;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using Unity.VisualScripting;

namespace Ads
{
    public interface IAdsResolver
    {
        void Initialize();
        void ShowInterstitialAd(Action callback);
        void ShowRewardInterstitialAd(Action<Reward> callback);
    }
}