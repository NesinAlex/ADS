using System.Collections;
using UnityEngine.SceneManagement;

namespace Devcell.Ads
{
    public abstract class BaseAdController : Singleton<BaseAdController>, IAdController
    {
        private protected abstract void RegisterAdService();

        public abstract void OnAdHelperReady(AdHelper adHelper);
        public abstract void OnAdHelperDestroyed();

        public abstract void ShowRewardedAd();
        public abstract void ShowInterstitialAd();
        public abstract void ShowBannerAd();
        public abstract void HideBannerAd();
        public abstract void OnSceneLoaded_HideBannerAd(Scene arg0, LoadSceneMode loadSceneMode);

        private protected abstract void TryGetRewardButton(Scene oldScene, Scene newScene);
        private protected abstract void HandleUserEarnedReward();
        private protected abstract void ResetAfterRewardPayed();
        private protected abstract void ResetParameters();
        private protected abstract IEnumerator CoroutineTryGetRewardButton(Scene newScene);
    }
}