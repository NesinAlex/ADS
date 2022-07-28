namespace Devcell.Ads
{
    public class GoogleAdService : AdService
    {
        protected override IBannerAdService GetBanner()
            => GetComponent<GoogleAdsBanner>();

        protected override IVideoAdService GetInterstitial()
            => GetComponent<GoogleAdsInterstitial>();

        protected override IRewardedVideoAdService GetReward()
            => GetComponent<GoogleAdsReward>();
    }
}