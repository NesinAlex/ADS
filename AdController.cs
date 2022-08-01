using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Devcell.Ads
{
    public class AdController : BaseAdController
    {
        [SerializeField] private GameObject unityAdServiceObj;
        [SerializeField] private GameObject googleAdServiceObj;
        [SerializeField] private GameObject yandexAdServiceObj;

        [SerializeField] private bool hideBannerOnSceneChanged = false;

        [SerializeField] private bool unityAdsEnabled;
        [SerializeField] private bool googleAdsEnabled;
        [SerializeField] private bool yandexAdsEnabled;

        private List<IAdService> _adServices = new List<IAdService>();
        //private bool _adIsShownSuccessful;
        private bool _interstitialAdAlreadyWasShown;

        private AdHelper _addHelper;

        int reward;

        public Action<int> OnGetReward { get; set; }

        private void Awake()
        {
            base.Awake();

            _interstitialAdAlreadyWasShown = false;
            SceneManager.activeSceneChanged += OnSceneChanged_Init;
            if (hideBannerOnSceneChanged)
                SceneManager.sceneLoaded += OnSceneLoaded_HideBannerAd;
        }

        private void Start()
        {
            RegisterAdService();
        }

        private protected override void RegisterAdService()
        {
            var unityAdService = unityAdServiceObj.GetComponent<UnityAdService>();
            var googleAdService = googleAdServiceObj.GetComponent<GoogleAdService>();
            var yandexAdService = yandexAdServiceObj.GetComponent<YandexAdService>();

            if (googleAdsEnabled /*&& Application.systemLanguage != SystemLanguage.Russian*/)
            {
                _adServices.Add(InitializeService(googleAdService));
                Debug.Log("Enable Google Ads Service");
            }

            if (unityAdsEnabled /*&& Application.systemLanguage != SystemLanguage.Russian*/)
            {
                _adServices.Add(InitializeService(unityAdService));
                Debug.Log("Enable Unity Ads Service");
            }

            if (yandexAdsEnabled)
            {
                _adServices.Add(InitializeService(yandexAdService));
                Debug.Log("Enable Yandex Ads Service");
            }

            _adServices.Sort();
        }

        private IAdService InitializeService(IAdService adService)
        {
            adService.Initialize();
            adService.OnRewardLoaded = OnRewardLoaded;
            adService.OnRewardLoadFail = OnRewardLoadFail;
            adService.OnUserEarnedReward = OnUserEarnReawrd;
            return adService;
        }

        public override void ShowRewardedAd()
        {
            foreach (IAdService adService in _adServices)
            {
                if (adService.PlayRewardedAd())
                {
                    HandleUserEarnedReward();
                    //_adIsShownSuccessful = true;
                    break;
                }
            }
        }

        public override void ShowInterstitialAd()
        {
            foreach (IAdService adService in _adServices)
            {
                if (adService.PlayVideoAd())
                {
                    if (_interstitialAdAlreadyWasShown)
                    {
                        return;
                    }

                    _interstitialAdAlreadyWasShown = true;
                    break;
                }
            }
        }

        public override void ShowBannerAd()
        {
            foreach (IAdService adService in _adServices)
            {
                if (adService.ShowBannerAd())
                {
                    break;
                }
            }
        }

        public override void HideBannerAd()
        {
            foreach (IAdService adService in _adServices)
            {
                if (adService.ShowBannerAd())
                {
                    break;
                }
            }
        }

        public override void OnSceneLoaded_HideBannerAd(Scene scene, LoadSceneMode loadSceneMode)
        {
            // client index
            if (scene.buildIndex != 0)
            {
                HideBannerAd();
            }
        }

        private void OnSceneChanged_Init(Scene oldScene, Scene newScene)
        {
            ResetParameters();
            TryGetRewardButton(oldScene, newScene);
            TryGetGameOverPanel(oldScene, newScene);
        }
        private void TryGetGameOverPanel(Scene oldScene, Scene newScene)
        {
            StartCoroutine(CoroutineTryGetGameOverPanel(oldScene, newScene));
        }
        private IEnumerator CoroutineTryGetGameOverPanel(Scene oldScene, Scene newScene)
        {
            yield return new WaitUntil(() => newScene.isLoaded);
        }
        private protected override void TryGetRewardButton(Scene oldScene, Scene newScene)
        {
            //StartCoroutine(CoroutineTryGetRewardButton(newScene));
        }

        private protected override IEnumerator CoroutineTryGetRewardButton(Scene newScene)
        {
            yield return new WaitUntil(() => newScene.isLoaded);
        }

        public override void OnAdHelperReady(AdHelper adHelper)
        {
            _addHelper = adHelper;

            _addHelper.GetRewardButtons().ForEach(btn => btn.onClick.AddListener(ShowRewardedAd));
            EnableRewardButtons(IsRewardAdLoaded());
        }

        public override void OnAdHelperDestroyed()
        {
            _addHelper = null;
        }

        private protected override void HandleUserEarnedReward()
        {
            //        _updateProfileProperty.UpdateProfileValues(ObservablePropertiyCodes.Gold, Rewards.Gold_Ads_Reward);

            //#if !PLATFORM_WEBGL
            //        FirebaseHandler.OnAdWatched(AdsType.Reward.ToString(), AdsPlatforms.Unity.ToString());
            //#endif

            ResetAfterRewardPayed();
        }

        private protected override void ResetAfterRewardPayed()
        {
            //_adIsShownSuccessful = false;
            EnableRewardButtons(false);
        }

        private protected override void ResetParameters()
        {
            _interstitialAdAlreadyWasShown = false;
        }

        private void EnableRewardButtons(bool isEnable)
        {
            if (_addHelper != null) { return; }

            foreach (var button in _addHelper.GetRewardButtons())
            {
                button.interactable = isEnable;
            }
        }

        private void OnRewardLoaded()
        {
            EnableRewardButtons(true);
        }

        private void OnRewardLoadFail(AdsPlatform platform)
        {
            EnableRewardButtons(IsRewardAdLoaded());
        }

        private bool IsRewardAdLoaded()
        {
            return _adServices.Any(s => s.IsRewardAdLoaded());
        }

        public void SetReward(int reward)
        {
            this.reward = reward;
        }

        public int CurrentReward() => reward;

        private void OnUserEarnReawrd() => OnGetReward?.Invoke(reward);
    }
}