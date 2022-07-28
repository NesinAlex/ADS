using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Devcell.Ads
{
    public class YandexAdsReward : MonoBehaviour, IRewardedVideoAdService
    {
        RewardedAd rewardedAd;

        public Action OnRewardLoaded { get; set; }
        public Action<AdsPlatform> OnRewardLoadFail { get; set; }
        public Action OnEarnedReward { get; set; }

        public string AdUnitId => AdsConstants.YandexRewardedId;

        public IRewardedVideoAdService Initialize()
        {
            RequestRewardedAd();
            return this;
        }

        public bool IsLoaded() => rewardedAd.IsLoaded();

        public bool TryPlayAd()
        {
            if (!rewardedAd.IsLoaded())
            {
                return false;
            }

            ShowAd();

            return true;
        }

        private void ShowAd()
        {
            this.rewardedAd.Show();
        }

        private void RequestRewardedAd()
        {
            if (this.rewardedAd != null)
            {
                this.rewardedAd.Destroy();
            }

            this.rewardedAd = new RewardedAd(AdUnitId);

            this.rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
            this.rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
            this.rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
            this.rewardedAd.OnLeftApplication += this.HandleLeftApplication;
            this.rewardedAd.OnAdClicked += this.HandleAdClicked;
            this.rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
            this.rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
            this.rewardedAd.OnImpression += this.HandleImpression;
            this.rewardedAd.OnRewarded += this.HandleRewarded;
            this.rewardedAd.OnRewardedAdFailedToShow += this.HandleRewardedAdFailedToShow;

            this.rewardedAd.LoadAd(this.CreateAdRequest());
        }

        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder().Build();
        }

        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
            OnRewardLoaded?.Invoke();
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
            OnRewardLoadFail?.Invoke(AdsPlatform.Yandex);
        }

        public void HandleRewardedAdShown(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdShown event received");
            RequestRewardedAd();
        }

        public void HandleReturnedToApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleReturnedToApplication event received");
        }

        public void HandleLeftApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleLeftApplication event received");
        }

        public void HandleAdClicked(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClicked event received");
        }

        public void HandleRewardedAdDismissed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdDismissed event received");
        }

        public void HandleImpression(object sender, ImpressionData impressionData)
        {
            var data = impressionData == null ? "null" : impressionData.rawData;
            MonoBehaviour.print("HandleImpression event received with data: " + data);
        }

        public void HandleRewarded(object sender, Reward args)
        {
            MonoBehaviour.print("HandleRewarded event received: amout = " + args.amount + ", type = " + args.type);
            OnEarnedReward?.Invoke();
        }

        public void HandleRewardedAdFailedToShow(object sender, AdFailureEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: " + args.Message);
        }
    }
}