using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Devcell.Ads
{
    public class YandexAdsInterstitial : MonoBehaviour, IVideoAdService
    {
        private Interstitial interstitial;

        public string AdUnitId => AdsConstants.YandexInterstitialId;

        public IVideoAdService Initialize()
        {
            RequestInterstitial();
            return this;
        }

        public bool TryPlayAd()
        {
            if (!interstitial.IsLoaded())
            {
                return false;
            }

            ShowAd();

            return true;
        }

        public void ShowAd()
        {
            interstitial.Show();
        }

        private void RequestInterstitial()
        {
            if (this.interstitial != null)
            {
                this.interstitial.Destroy();
            }

            this.interstitial = new Interstitial(AdUnitId);

            //this.interstitial.OnInterstitialLoaded += this.HandleInterstitialLoaded;
            //this.interstitial.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
            //this.interstitial.OnReturnedToApplication += this.HandleReturnedToApplication;
            //this.interstitial.OnLeftApplication += this.HandleLeftApplication;
            //this.interstitial.OnAdClicked += this.HandleAdClicked;
            this.interstitial.OnInterstitialShown += this.HandleInterstitialShown;
            //this.interstitial.OnInterstitialDismissed += this.HandleInterstitialDismissed;
            //this.interstitial.OnImpression += this.HandleImpression;
            //this.interstitial.OnInterstitialFailedToShow += this.HandleInterstitialFailedToShow;

            this.interstitial.LoadAd(this.CreateAdRequest());
        }

        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder().Build();
        }

        private void HandleInterstitialShown(object sender, EventArgs e)
        {
            RequestInterstitial();
        }
    }
}