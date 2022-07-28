using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace Devcell.Ads
{
    public class GoogleAdsReward : MonoBehaviour, IRewardedVideoAdService
    {
        private RewardedAd _rewardedAd;

        private bool _wasShown;

        public Action OnRewardLoaded { get; set; }
        public Action<AdsPlatform> OnRewardLoadFail { get; set; }
        public Action OnEarnedReward { get; set; }

        public string AdUnitId => AdsConstants.GoogleMobileAdsId;

        public IRewardedVideoAdService Initialize()
        {
            RequestAndLoad();

            return this;
        }

        public void ShowAd()
        {
            _rewardedAd.Show();
        }

        public bool TryPlayAd()
        {
            if (!_rewardedAd.IsLoaded())
            {
                return false;
            }

            ShowAd();

            return true;
        }

        private void RequestAndLoad()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
            }

            // Initialize an _rewardedAd. (Объект должен создаваться перед показом каждой отдельной рекламы)
            this._rewardedAd = new RewardedAd(AdUnitId);

            // загружать рекламу в этом событии. Рекомендация google
            this._rewardedAd.OnAdFailedToLoad += OnAdFailedToLoad;
            //this._rewardedAd.OnAdFailedToShow += InterstitialAd_OnAdFailedToShow;
            //this._rewardedAd.OnAdOpening += InterstitialAd_OnAdOpening;
            this._rewardedAd.OnAdClosed += OnAdClosed;
            this._rewardedAd.OnAdClosed += OnAdLoaded;
            this._rewardedAd.OnUserEarnedReward += OnUserEarnedReward;
            //this._rewardedAd.OnPaidEvent += Interstitial_OnPaidEvent;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this._rewardedAd.LoadAd(request);
        }

        private void OnUserEarnedReward(object sender, Reward e)
        {
            OnEarnedReward?.Invoke();
        }

        private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            OnRewardLoadFail?.Invoke(AdsPlatform.Google);
        }

        private void OnAdClosed(object sender, System.EventArgs e)
        {
            RequestAndLoad();
        }

        private void OnAdLoaded(object sender, System.EventArgs e)
        {
            Debug.Log("rewarded ad - loaded");
            OnRewardLoaded?.Invoke();
        }

        public bool IsLoaded() => _rewardedAd.IsLoaded();
    }
}