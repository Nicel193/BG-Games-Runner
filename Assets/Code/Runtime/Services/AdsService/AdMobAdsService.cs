using System;
using Code.Runtime.Services.LogService;
using GoogleMobileAds.Api;

namespace Code.Runtime.Services.AdsService
{
    public class AdMobAdsService : IAdsService
    {
        private const string appId = "ca-app-pub-1385093244148841~5602672977";

        public bool IsRewardAdLoaded { get; private set; }

#if UNITY_ANDROID
        string bannerId = "ca-app-pub-1385093244148841/2952458907";
        string interId = "ca-app-pub-3940256099942544/1033173712";
        string rewardedId = "ca-app-pub-3940256099942544/5224354917";
        string nativeId = "ca-app-pub-3940256099942544/2247696110";

#elif UNITY_IPHONE
    string bannerId = "ca-app-pub-3940256099942544/2934735716";
    string interId = "ca-app-pub-3940256099942544/4411468910";
    string rewardedId = "ca-app-pub-3940256099942544/1712485313";
    string nativeId = "ca-app-pub-3940256099942544/3986624511";
#endif

        private RewardedAd _rewardedAd;
        private ILogService _logService;

        public AdMobAdsService(ILogService logService)
        {
            _logService = logService;
        }

        public void Initialize()
        {
            MobileAds.Initialize(initStatus => {
                _logService.Log("Ads Initialised !!");
        
                LoadRewardedAd();
            });
        }

        private void LoadRewardedAd()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");

            RewardedAd.Load(rewardedId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    
                    _logService.Log("Rewarded failed to load" + error);
                    return;
                }

                _logService.Log("Rewarded ad loaded !!");
                _rewardedAd = ad;
                IsRewardAdLoaded = true;
                RewardedAdEvents(_rewardedAd);
            });
        }

        public void ShowRewardedAd(Action onVideoFinished)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show(reward =>
                {
                    IsRewardAdLoaded = false;
                    
                    onVideoFinished?.Invoke();

                    LoadRewardedAd();
                    
                    _logService.Log("Ad video finished");
                });
            }
            else
            {
                _logService.Log("Rewarded ad not ready");
            }
        }

        public void RewardedAdEvents(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                _logService.Log("Rewarded ad paid {0} {1}." +
                          adValue.Value +
                          adValue.CurrencyCode);
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () => { _logService.Log("Rewarded ad recorded an impression."); };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () => { _logService.Log("Rewarded ad was clicked."); };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () => { _logService.Log("Rewarded ad full screen content opened."); };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () => { _logService.Log("Rewarded ad full screen content closed."); };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                _logService.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);
            };
        }
    }
}