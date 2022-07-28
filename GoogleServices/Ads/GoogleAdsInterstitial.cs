using GoogleMobileAds.Api;
using UnityEngine;

namespace Devcell.Ads
{
    public class GoogleAdsInterstitial : MonoBehaviour, IVideoAdService
    {
        private InterstitialAd _interstitialAd;

        private bool _wasShown;

        public string AdUnitId => AdsConstants.GoogleMobileAdsId;

        public IVideoAdService Initialize()
        {
            RequestAndLoad();

            return this;
        }

        public void ShowAd()
        {
            _interstitialAd.Show();
        }

        public bool TryPlayAd()
        {
            if (!_interstitialAd.IsLoaded())
            {
                return false;
            }

            ShowAd();

            return true;
        }

        private void RequestAndLoad()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
            }

            // Initialize an _interstitialAd. (Объект должен создаваться перед показом каждой отдельной рекламы)
            this._interstitialAd = new InterstitialAd(AdUnitId);

            // загружать рекламу в этом событии. Рекомендация google
            //this._interstitialAd.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;
            //this._interstitialAd.OnAdFailedToShow += InterstitialAd_OnAdFailedToShow;
            //this._interstitialAd.OnAdOpening += InterstitialAd_OnAdOpening;
            //this._interstitialAd.OnAdLoaded += OnAdLoaded;
            this._interstitialAd.OnAdClosed += OnAdCloded;
            //this._interstitialAd.OnPaidEvent += Interstitial_OnPaidEvent;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this._interstitialAd.LoadAd(request);
        }

        private void OnAdCloded(object sender, System.EventArgs e)
        {
            RequestAndLoad();
        }
    }
}