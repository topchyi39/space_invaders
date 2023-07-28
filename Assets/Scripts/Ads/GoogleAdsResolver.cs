using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class GoogleAdsResolver : MonoBehaviour, IAdsResolver
    {
        [SerializeField] private string interstitialAdId;
        [SerializeField] private string rewardInterstitialAdId;
        
        private bool _initialized;

        private InterstitialAd _interstitialAd;
        private RewardedInterstitialAd _rewardedInterstitialAd;
        
        public void Initialize()
        {
            GoogleMobileAds.Api.MobileAds.RaiseAdEventsOnUnityMainThread = true;
            GoogleMobileAds.Api.MobileAds.Initialize(InitializeAds);
        }
        
        public async void ShowInterstitialAd(Action callback)
        {
            await UniTask.WaitUntil(() => _initialized);
            
            if (_interstitialAd == null || !_interstitialAd.CanShowAd()) return;
            
            _interstitialAd.Show();
            _interstitialAd.OnAdFullScreenContentClosed += callback;
            _interstitialAd.OnAdFullScreenContentClosed += DisposeInterstitialAd;
        }
        
        public async void ShowRewardInterstitialAd(Action<Reward> callback)
        {
            await UniTask.WaitUntil(() => _initialized);

            if (_rewardedInterstitialAd == null || !_rewardedInterstitialAd.CanShowAd())
                await LoadRewardInterstitialAd();
            
            _rewardedInterstitialAd.Show(callback);
            _rewardedInterstitialAd.OnAdFullScreenContentClosed += DisposeInterstitialAd;
        }

        private async void InitializeAds(InitializationStatus initializationStatus)
        {
            var tasks = new[]
            {
                LoadInterstitialAd(),
                LoadRewardInterstitialAd()
            };
            
            await UniTask.WhenAll(tasks);

            _initialized = true;
        }

        private async UniTask LoadRewardInterstitialAd()
        {
            if (_rewardedInterstitialAd != null)
            {
                _rewardedInterstitialAd.Destroy();
                _rewardedInterstitialAd = null;
            }
            
            var loaded = false;
            
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");
            RewardedInterstitialAd.Load(rewardInterstitialAdId, adRequest, 
                (ad, error) =>
                {
                    loaded = true;
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }
                                
                    _rewardedInterstitialAd = ad;
                } );

            await UniTask.WaitUntil(() => loaded);
        }

        private async UniTask LoadInterstitialAd()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            var loaded = false;
            
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");
            InterstitialAd.Load(interstitialAdId, adRequest, 
                (ad, error) =>
                            {
                                loaded = true;
                                if (error != null || ad == null)
                                {
                                    Debug.LogError("interstitial ad failed to load an ad " +
                                                   "with error : " + error);
                                    return;
                                }
                                
                                _interstitialAd = ad;
                            } );

            await UniTask.WaitUntil(() => loaded);
        }
        
        private void DisposeRewardInterstitialAd()
        {
            _rewardedInterstitialAd.Destroy();
            _rewardedInterstitialAd = null;
            LoadRewardInterstitialAd();
        }
        
        private void DisposeInterstitialAd()
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
            LoadInterstitialAd();
        }
    }
}