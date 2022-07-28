using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Devcell.Ads
{
    public class UnityAdsBanner : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener,
        IUnityAdsShowListener, IBannerAdService
    {
        [SerializeField] private bool _testMode = true;

        private readonly string _banner = "Banner_Android";

        private readonly string _gameId = "4777617";

        private void Start()
        {
            Advertisement.Initialize(_gameId, _testMode);
        }

        public void OnInitializationComplete()
        {
            throw new NotImplementedException();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
                Debug.Log("Unity Ad skipped");
            else if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                Debug.Log("Unity Ad completed");
            else if (showCompletionState == UnityAdsShowCompletionState.UNKNOWN) Debug.Log("Unity Ad unknown error");
        }

        public void ShowBanner(object sender, AdFailedToLoadEventArgs adFailedToLoadEventArgs)
        {
            ShowBanner();
        }

        public bool ShowBanner()
        {
            StartCoroutine(ShowBannerWhenInitialized());
            return true;
        }

        private IEnumerator ShowBannerWhenInitialized()
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

            while (!Advertisement.isInitialized) yield return new WaitForSeconds(0.5f);
            Advertisement.Banner.Show(_banner);
        }

        public bool HideBanner()
        {
            Advertisement.Banner.Hide(true);
            return true;
        }
    }
}