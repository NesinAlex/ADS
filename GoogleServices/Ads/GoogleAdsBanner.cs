using GoogleMobileAds.Api;
using UnityEngine;

namespace Devcell.Ads
{
    public class GoogleAdsBanner : MonoBehaviour, IBannerAdService
    {
        private BannerView _bannerView;
        private UnityAdsBanner _unityAdsBanner;

        public void Start()
        {
            _unityAdsBanner = FindObjectOfType<UnityAdsBanner>();

            if (PlayerPrefs.HasKey("ads") && PlayerPrefs.GetInt("ads") == 0) return;

            MobileAds.Initialize(initStatus => { });

            RequestBanner();
        }

        private void RequestBanner()
        {
#if UNITY_ANDROID
        var adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

            _bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

            var request = new AdRequest.Builder().Build();

            _bannerView.LoadAd(request);

            _bannerView.Hide();

            _bannerView.OnAdFailedToLoad += (sender, args) => _unityAdsBanner.ShowBanner();
        }

        public bool ShowBanner()
        {
            if (_bannerView == null) return false;

            _bannerView.Show();

            return true;
        }

        public bool HideBanner()
        {
            if (_bannerView == null) return false;

            _bannerView.Hide();

            return true;
        }
    }
}