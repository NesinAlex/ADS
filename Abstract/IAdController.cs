using UnityEngine.SceneManagement;

namespace Devcell.Ads
{
    public interface IAdController
    {
        void ShowRewardedAd();

        void ShowInterstitialAd();

        void ShowBannerAd();

        void HideBannerAd();

        void OnSceneLoaded_HideBannerAd(Scene arg0, LoadSceneMode loadSceneMode);
    }
}
