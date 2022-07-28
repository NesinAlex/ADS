namespace Devcell.Ads
{
    public class YandexAdService : AdService
    {
        protected override IBannerAdService GetBanner() =>
            GetComponent<YandexAdsBanner>();

        protected override IVideoAdService GetInterstitial() =>
            GetComponent<YandexAdsInterstitial>();

        protected override IRewardedVideoAdService GetReward() =>
            GetComponent<YandexAdsReward>();
    }
}