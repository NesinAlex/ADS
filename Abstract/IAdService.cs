using System;

namespace Devcell.Ads
{
    public interface IAdService
    {
        Action OnRewardLoaded { get; set; }
        Action<AdsPlatform> OnRewardLoadFail { get; set; }
        Action OnUserEarnedReward { get; set; }
        IAdService Initialize();
        bool PlayRewardedAd();
        bool PlayVideoAd();
        bool ShowBannerAd();
        bool HideBannerAd();
        bool IsRewardAdLoaded();
    }
}
