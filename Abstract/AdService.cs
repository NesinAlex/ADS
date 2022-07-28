using System;
using UnityEngine;

namespace Devcell.Ads
{
    public abstract class AdService : MonoBehaviour, IAdService
    {
        private IRewardedVideoAdService _reward;
        private IVideoAdService _interstitial;
        private IBannerAdService _banner;

        IRewardedVideoAdService Reward
        {
            get
            {
                if (_reward == null) { _reward = GetReward(); }
                return _reward;
            }
        }
        IVideoAdService Interstitial
        {
            get
            {
                if (_interstitial == null) { _interstitial = GetInterstitial(); }
                return _interstitial;
            }
        }
        IBannerAdService Banner
        {
            get
            {
                if (_banner == null) { _banner = GetBanner(); }
                return _banner;
            }
        }


        protected abstract IRewardedVideoAdService GetReward();
        protected abstract IVideoAdService GetInterstitial();
        protected abstract IBannerAdService GetBanner();

        Action IAdService.OnRewardLoaded
        {
            get => Reward.OnRewardLoaded;
            set => Reward.OnRewardLoaded = value;
        }

        public Action<AdsPlatform> OnRewardLoadFail
        {
            get => Reward.OnRewardLoadFail;
            set => Reward.OnRewardLoadFail = value;
        }

        public Action OnUserEarnedReward
        {
            get => Reward.OnEarnedReward;
            set => Reward.OnEarnedReward = value;
        }

        public IAdService Initialize()
        {
            Reward?.Initialize();
            Interstitial?.Initialize();
            Banner?.Initialize();
            return this;
        }

        public bool PlayRewardedAd()
        {
            return Reward.TryPlayAd();
        }

        public bool IsRewardAdLoaded()
        {
            return Reward.IsLoaded();
        }

        public bool PlayVideoAd()
        {
            return Interstitial.TryPlayAd();
        }

        public bool ShowBannerAd()
        {
            return Banner.ShowBanner();
        }

        public bool HideBannerAd()
        {
            return Banner.HideBanner();
        }

        private void OnDestroy()
        {
            _reward = null;
            _interstitial = null;
            _banner = null;
        }
    }
}
