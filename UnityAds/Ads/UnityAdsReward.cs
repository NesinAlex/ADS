using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Devcell.Ads
{
    public class UnityAdsReward : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener,
        IUnityAdsShowListener, IRewardedVideoAdService
    {
        private string _androidGameId = AdsConstants.UnityAdAndroidGameId;
        private string _iOSGameId = AdsConstants.UnityAdIOSGameId;
        private bool _testMode = AdsConstants.UnityAdTestMode;

        private string _androidAdUnitId = AdsConstants.UnityAndroidRewardedAd;
        private string _iOSAdUnitId = AdsConstants.UnityIOSRewardedAd;

        private string _adUnitId;
        private string _gameId;

        bool isLoaded;

        public Action OnRewardLoaded { get; set; }
        public Action<AdsPlatform> OnRewardLoadFail { get; set; }
        public Action OnEarnedReward { get; set; }

        public string AdUnitId => throw new NotImplementedException();

        public IRewardedVideoAdService Initialize()
        {
            InitializeAds();

            Debug.Log("Awake");
            _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSAdUnitId
                : _androidAdUnitId;

            Debug.Log("the _adUnitId is: " + _adUnitId);

            return this;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
            if (adUnitId.Equals(_adUnitId))
            {
                OnRewardLoaded?.Invoke();
                isLoaded = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            if (adUnitId.Equals(_adUnitId))
            {
                OnRewardLoadFail?.Invoke(AdsPlatform.Unity);
            }
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
                OnEarnedReward?.Invoke();

                // Load another ad:
                Advertisement.Load(_adUnitId, this);
            }
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        public void InitializeAds()
        {
            _gameId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
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

        public bool IsLoaded() => isLoaded;
    }
}