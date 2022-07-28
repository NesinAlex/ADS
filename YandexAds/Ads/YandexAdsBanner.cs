using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Devcell.Ads
{
    public class YandexAdsBanner : MonoBehaviour, IBannerAdService
    {
        readonly string adUnitId = AdsConstants.YandexBanerdId;

        private Banner banner;

        public void Initialize()
        {
            RequestBanner();
        }

        public bool ShowBanner()
        {
            if (banner == null) return false;

            banner.Show();

            return true;
        }

        public bool HideBanner()
        {
            if (banner == null) return false;

            banner.Hide();

            return true;
        }

        private void RequestBanner()
        {
            if (this.banner != null)
            {
                this.banner.Destroy();
            }

            // Set flexible banner maximum width and height
            AdSize bannerMaxSize = AdSize.FlexibleSize(GetScreenWidthDp(), 100);
            // Or set sticky banner maximum width
            //AdSize bannerMaxSize = AdSize.StickySize(GetScreenWidthDp());
            this.banner = new Banner(adUnitId, bannerMaxSize, AdPosition.BottomCenter);

            //this.banner.OnAdLoaded += this.HandleAdLoaded;
            //this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
            //this.banner.OnReturnedToApplication += this.HandleReturnedToApplication;
            //this.banner.OnLeftApplication += this.HandleLeftApplication;
            //this.banner.OnAdClicked += this.HandleAdClicked;
            //this.banner.OnImpression += this.HandleImpression;

            this.banner.LoadAd(this.CreateAdRequest());
        }

        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder().Build();
        }

        // Example how to get screen width for request
        private int GetScreenWidthDp()
        {
            int screenWidth = (int)Screen.safeArea.width;
            return ScreenUtils.ConvertPixelsToDp(screenWidth);
        }
    }
}