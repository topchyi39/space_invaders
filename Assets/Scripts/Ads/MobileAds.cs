using System;
using Analytics;
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

namespace Ads
{
    public class MobileAds : MonoBehaviour
    {
        private IAdsResolver _resolver;
        private GameAnalytics _analytics;
        
        [Inject]
        private void Construct(IAdsResolver adsResolver, GameAnalytics analytics)
        {
            _resolver = adsResolver;
            _analytics = analytics;
        }

        private void Awake()
        {
            _resolver.Initialize();
        }
        
        public void ShowInterstitialAd(Action callback)
        {
            _resolver.ShowInterstitialAd(()=>
            {
                _analytics.LogAdInterstitial();
                callback?.Invoke();
            });
        }
        
        public void ShowRewardInterstitialAd(Action<Reward> callback)
        {
            _resolver.ShowRewardInterstitialAd(reward =>
            {
                _analytics.LogAdRewardInterstitial();
                callback?.Invoke(reward);
            });
        }
    }
}