//https://docs.unity.com/ads/InstallingTheUnitySDK.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
#if GEM_UNITY_AD
namespace Gemmob.Lib.Adsv2
{
    public class AdMediationUnity : AdMediationFramework, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        bool _testMode = false;///////////////////////////////////
        private string _gameId;

        private string BANNER_ID = "";
        private string INTERSTITIAL_ID = "";
        private string REWARD_AD_ID = "";
        private bool bannerLoadedFlag = false, interstitialLoadedFlag = false, rewardAdLoadedFlag = false;


        public override void Initialization(System.Action<AdsEventType, AdMediationFramework> OnAdCallback, AdsConfig adsConfig)
        {
            base.Initialization(OnAdCallback, adsConfig);
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? adsConfig.unityGameIdIos : adsConfig.unityGameIdAndroid;
            Advertisement.Initialize(_gameId, _testMode, this);

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                BANNER_ID = "Banner_iOS";
                INTERSTITIAL_ID = "Interstitial_iOS";
                REWARD_AD_ID = "Rewarded_iOS";
            }
            else
            {
                BANNER_ID = "Banner_Android";
                INTERSTITIAL_ID = "Interstitial_Android";
                REWARD_AD_ID = "Rewarded_Android";
            }
        }


        public override void RequestBanner()
        {
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = () =>
                { //OnBannerLoaded,
                    bannerLoadedFlag = true;
                    OnAdCallback(AdsEventType.BannerLoaded, this);
                },
                errorCallback = (errMessage) =>
                {  //OnBannerError
                    bannerLoadedFlag = false;
                    Debug.Log($"Load unity banner failed: {errMessage}");
                    OnAdCallback(AdsEventType.BannerFailedToLoad, this);
                }
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(BANNER_ID, options);
        }

        public override void RequestInterstitial()
        {
            interstitialLoadedFlag = false;
            // IMPORTANT! Only load content AFTER initialization
            Advertisement.Load(INTERSTITIAL_ID, this);
        }

        public override void RequestRewardBasedVideo()
        {
            rewardAdLoadedFlag = false;
            // IMPORTANT! Only load content AFTER initialization
            Advertisement.Load(REWARD_AD_ID, this);
        }

        public override void ShowBanner()
        {
            // Set up options to notify the SDK of show events:
            BannerOptions options = new BannerOptions
            {
                clickCallback = () => { },//OnBannerClicked,
                hideCallback = () => { },//OnBannerHidden,
                showCallback = () => { }//OnBannerShown
            };

            // Show the loaded Banner Ad Unit:
            Advertisement.Banner.Show(BANNER_ID, options);
        }

        public override void ShowInterstitial()
        {
            Advertisement.Show(INTERSTITIAL_ID, this);
        }

        public override void ShowRewardBasedVideo()
        {
            Advertisement.Show(REWARD_AD_ID, this);
        }

        public override bool IsBannerLoaded()
        {
            return bannerLoadedFlag;
        }

        public override bool IsInterstitialLoaded()
        {
            return interstitialLoadedFlag;
        }

        public override bool IsRewardBasedVideoLoaded()
        {
            return rewardAdLoadedFlag;
        }

        public override void DestroyBanner()
        {
            Advertisement.Banner.Hide(true);
        }

        public override void HideBanner()
        {
            Advertisement.Banner.Hide(false);
        }

        public override void ShowMediationTestSuite() { }




#region Interface Implementations
        public void OnInitializationComplete()
        {
            Debug.Log("Init Success");
            OnAdCallback(AdsEventType.OnSDKInitialized, this);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Init Failed: [{error}]: {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"Load Success: {placementId}");
            if (placementId.Equals(REWARD_AD_ID))
            {
                rewardAdLoadedFlag = true;
                OnAdCallback(AdsEventType.RewardAdLoaded, this);
            }
            else if (placementId.Equals(INTERSTITIAL_ID))
            {
                interstitialLoadedFlag = true;
                OnAdCallback(AdsEventType.InterstitialLoaded, this);
            }
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Load Failed: [{error}:{placementId}] {message}");
            if (placementId.Equals(REWARD_AD_ID))
            {
                rewardAdLoadedFlag = false;
                OnAdCallback(AdsEventType.RewardAdFailedToLoad, this);
            }
            else if (placementId.Equals(INTERSTITIAL_ID))
            {
                interstitialLoadedFlag = false;
                OnAdCallback(AdsEventType.InterstitialFailedToLoad, this);
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"OnUnityAdsShowFailure: [{error}]: {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"OnUnityAdsShowStart: {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"OnUnityAdsShowClick: {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
            if (placementId.Equals(REWARD_AD_ID))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
                    OnAdCallback(AdsEventType.RewardAdEarned, this);
                OnAdCallback(AdsEventType.RewardAdClosed, this);
            }
            else if (placementId.Equals(INTERSTITIAL_ID)) OnAdCallback(AdsEventType.InterstitialClosed, this);
        }
#endregion



    }
}

#endif