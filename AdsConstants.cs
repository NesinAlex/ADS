using UnityEngine;

namespace Devcell.Ads
{
    public static class AdsConstants
    {
        // UnityAds
        public static string UnityAdAndroidGameId = "4570777";
        public static string UnityAdIOSGameId = "4570776";
        public static bool UnityAdTestMode = true;
        public static string UnityAndroidRewardedAd = "Rewarded_Android";
        public static string UnityIOSRewardedAd = "Rewarded_iOS";
        public static string UnityAndroidVideoAd = "Interstitial_Android";
        public static string UnityIOSVideoAd = "Interstitial_iOS";

        // AdMob
        public static string GoogleMobileAdsId
        {
            get
            {
                var testId = "ca-app-pub-8521914915885270~1844449613";
                var iOsId = "ca-app-pub-8521914915885270~1844449613";
                var androidId = "ca-app-pub-8521914915885270~1844449613";
                return GetId(testId: testId, iOsId: iOsId, androidId: androidId);
            }
        }

        public static string YandexInterstitialId
        {
            get
            {
                var testId = "R-M-DEMO-interstitial";
                var iOsId = "R-M-1760333-1";
                var androidId = "R-M-1760111-1";
                return GetId(testId: testId, iOsId: iOsId, androidId: androidId);
            }
        }

        public static string YandexRewardedId
        {
            get
            {
                var testId = "R-M-DEMO-rewarded-client-side-rtb";
                var iOsId = "R-M-1760333-2";
                var androidId = "R-M-1760111-2";
                return GetId(testId: testId, iOsId: iOsId, androidId: androidId);
            }
        }

        public static string YandexBanerdId
        {
            get
            {
                // Replace demo R-M-DEMO-320x50 with actual Ad Unit ID
                // Following demo Block IDs may be used for testing:
                // R-M-DEMO-320x50
                // R-M-DEMO-320x100
                var testId = "R-M-DEMO-320x100";
                var iOsId = "";
                var androidId = "";
                return GetId(testId: testId, iOsId: iOsId, androidId: androidId);
            }
        }

        private static string GetId(string testId, string iOsId, string androidId)
        {
            switch (CurrentBuild())
            {
                case AppBuild.Test:
                    return testId;
                case AppBuild.Android:
                    return androidId;
                case AppBuild.IOS:
                    return iOsId;
            }

            // by default return andoridId;
            return androidId;
        }

        public static AppBuild CurrentBuild()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.LinuxEditor ||
                Application.platform == RuntimePlatform.OSXEditor ||
                Debug.isDebugBuild)
            {
                return AppBuild.Test;
            }
            else if (Application.platform == RuntimePlatform.OSXPlayer ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return AppBuild.IOS;
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                return AppBuild.Android;
            }

            return AppBuild.Android;
        }
    }

    public enum AdsType
    {
        Reward,
        Banner,
        Video
    }

    public enum AdsPlatform
    {
        Google,
        Unity,
        Yandex
    }

    public enum AppBuild
    {
        Test = 0,
        Android = 1,
        IOS = 2
    }
}