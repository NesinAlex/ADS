
namespace Devcell.Ads
{
    public class UnityAdService : AdService
    {
        protected override IBannerAdService GetBanner()
            => GetComponent<UnityAdsBanner>();

        protected override IVideoAdService GetInterstitial()
            => GetComponent<UnityAdsInterstitial>();

        protected override IRewardedVideoAdService GetReward()
            => GetComponent<UnityAdsReward>();
    }
}