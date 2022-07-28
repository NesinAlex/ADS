using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Devcell.Ads
{

    public class UnityAdsInterstitial : MonoBehaviour, IVideoAdService, IUnityAdsInitializationListener, IUnityAdsLoadListener,
    IUnityAdsShowListener
    {

        private readonly string _androidGameId = AdsConstants.UnityAdAndroidGameId;
        private readonly string _iOSGameId = AdsConstants.UnityAdIOSGameId;
        private readonly bool _testMode = AdsConstants.UnityAdTestMode;

        private readonly string _androidAdUnitId = AdsConstants.UnityAndroidVideoAd;
        private readonly string _iOSAdUnitId = AdsConstants.UnityIOSVideoAd;

        private string _adUnitId;
        private string _gameId;

        bool isLoaded;

        public string AdUnitId => throw new NotImplementedException();

        public IVideoAdService Initialize()
        {
            InitializeAds();

            Debug.Log("Awake");
            _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSAdUnitId
                : _androidAdUnitId;

            Debug.Log("the _adUnitId is: " + _adUnitId);

            return this;
        }

        public void InitializeAds()
        {
            _gameId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public bool TryPlayAd()
        {
            if (isLoaded)
            {
                isLoaded = false;
                Advertisement.Show(_adUnitId, this);
                return true;
            }

            return false;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            LoadAd();
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity ad initizliation fail: {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"Unity ad loaded: {placementId}");
            isLoaded = true;
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Unity ad load fail: {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Unity ad show fail: {message}");
        }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
    }
}