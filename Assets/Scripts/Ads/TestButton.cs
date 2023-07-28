
using System;
using Analytics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ads
{
    public class TestButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private MobileAds _mobileAds;
        private GameAnalytics _gameAnalytics;
        
        [Inject]
        private void Construct(MobileAds ads,  GameAnalytics analytics)
        {
            _mobileAds = ads;
            _gameAnalytics = analytics;
        }

        private void Awake()
        {
            button.onClick.AddListener(() => _mobileAds.ShowInterstitialAd(null));
        }
    }
}