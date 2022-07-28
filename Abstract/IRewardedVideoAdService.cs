using System;

namespace Devcell.Ads
{
    public interface IRewardedVideoAdService
    {
        string AdUnitId { get; }
        Action OnRewardLoaded { get; set; }
        Action<AdsPlatform> OnRewardLoadFail { get; set; }
        Action OnEarnedReward { get; set; }
        IRewardedVideoAdService Initialize();
        bool TryPlayAd();
        bool IsLoaded();
    }
}
