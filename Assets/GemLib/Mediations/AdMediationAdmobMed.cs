//https://developers.google.com/admob/unity/quick-start#initialize_the_mobile_ads_sdk
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using GoogleMobileAdsMediationTestSuite.Api;

#if GEM_ADMOB_MED || GEM_ADMOB_NATIVE
namespace Gemmob.Lib.Adsv2
{
    public class AdMediationAdmobMed : AdMediationFramework
    {
        private BannerView bannerView;
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;

        public AdMediationAdmobMed()
        {
            MobileAds.SetiOSAppPauseOnBackground(true);
            List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

            // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
            deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
            //deviceIds.Add("95f23fa0695d4e749a49cf136ab9a133");
            deviceIds.Add("3e4f108909e9479cab8304994883f975");
#endif

            // Configure TagForChildDirectedTreatment and test device IDs.
            RequestConfiguration requestConfiguration =
                new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
                .SetTestDeviceIds(deviceIds).build();
            MobileAds.SetRequestConfiguration(requestConfiguration);

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(HandleInitCompleteAction);
        }

        public override void Initialization(System.Action<AdsEventType, AdMediationFramework> OnAdCallback, AdsConfig adsConfig)
        {
            base.Initialization(OnAdCallback, adsConfig);
            OnAdCallback(AdsEventType.OnSDKInitialized, this);
        }

        private void HandleInitCompleteAction(InitializationStatus initstatus)
        {
            Debug.Log("Initialization complete.");

            // Callbacks from GoogleMobileAds are not guaranteed to be called on the main thread.
            // In this example we use MobileAdsEventExecutor to schedule these calls on the next Update() loop.
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                //RequestBanner();
                Debug.LogError("Test init log");
            });
        }

        public override void RequestBanner()
        {
            string adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ?
                adsConfig.admobConfig.bannerMediationIos : adsConfig.admobConfig.bannerMediationAndroid;
            LoadBanner(ref this.bannerView, adUnitId, OnAdCallback, this);
        }

        public override void RequestInterstitial()
        {
            string adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ?
                adsConfig.admobConfig.interstitialMediationIos : adsConfig.admobConfig.interstitialMediationAndroid;
            LoadInterstitial(ref this.interstitial, adUnitId, OnAdCallback, this);
        }

        public override void RequestRewardBasedVideo()
        {
            string adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ?
                adsConfig.admobConfig.rewardAdMediationIos : adsConfig.admobConfig.rewardAdMediationAndroid;
            LoadRewardedAd(ref rewardedAd, adUnitId, OnAdCallback, this);
        }


        public override void ShowBanner()
        {
            if (this.bannerView != null)
            {
                this.bannerView.SetPosition(AdPosition.Bottom);
                this.bannerView.Show();
            }
        }

        public override void ShowInterstitial()
        {
            if (IsInterstitialLoaded()) interstitial.Show();
        }

        public override void ShowRewardBasedVideo()
        {
            if (IsRewardBasedVideoLoaded()) rewardedAd.Show();
        }


        public override bool IsBannerLoaded()
        {
            return bannerView != null;
        }

        public override bool IsInterstitialLoaded()
        {
            if (interstitial != null) Debug.Log("admobMed, interstitial loaded: " + interstitial.IsLoaded());
            else Debug.Log("interstitial is null");

            if (interstitial != null && interstitial.IsLoaded()) return true;
            return false;
        }

        public override bool IsRewardBasedVideoLoaded()
        {
            return rewardedAd != null;
        }

        public override void DestroyBanner()
        {
            if (this.bannerView != null)
            {
                this.bannerView.Destroy();
            }
        }

        public override void HideBanner()
        {
            if (this.bannerView != null)
            {
                this.bannerView.Hide();
            }
        }



        public override void ShowMediationTestSuite() {
            Debug.Log(">>>>>show medation test suite!");
            MediationTestSuite.Show();
        }




        public void LoadBanner(ref BannerView bannerView, string adUnitId, Action<AdsEventType, AdMediationFramework> OnAdCallback, AdMediationFramework adMediation)
        {
            // Clean up banner ad before creating a new one.
            if (bannerView != null) bannerView.Destroy();
            // Create a 320x50 banner at the top of the screen.
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

            // Add Event Handlers
            bannerView.OnAdLoaded += (sender, args) =>
            {
                PrintStatus("Banner ad loaded.");
                OnAdCallback(AdsEventType.BannerLoaded, adMediation);
            };
            bannerView.OnAdFailedToLoad += (sender, args) =>
            {
                PrintStatus("Banner ad failed to load with error: " + args.LoadAdError.GetMessage());
                OnAdCallback(AdsEventType.BannerFailedToLoad, adMediation);
            };
            bannerView.OnAdOpening += (sender, args) =>
            {
                PrintStatus("Banner ad opening.");
            };
            bannerView.OnAdClosed += (sender, args) =>
            {
                PrintStatus("Banner ad closed.");
            };
            bannerView.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}", "Banner ad received a paid event.",
                                            args.AdValue.CurrencyCode, args.AdValue.Value);
                PrintStatus(msg);
            };


            // Load the banner with the request.
            bannerView.LoadAd(CreateAdRequest());
        }

        public void LoadInterstitial(ref InterstitialAd interstitial, string adUnitId, Action<AdsEventType, AdMediationFramework> OnAdCallback, AdMediationFramework adMediation)
        {
            // Clean up interstitial ad before creating a new one.
            if (interstitial != null) interstitial.Destroy();
            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId); // On iOS, InterstitialAd objects are one time use objects. That means once an interstitial is shown, the InterstitialAd object can't be used to load another ad.

            // Add Event Handlers
            interstitial.OnAdLoaded += (sender, args) =>
            {
                PrintStatus("Interstitial ad loaded.");
                OnAdCallback(AdsEventType.InterstitialLoaded, adMediation);
            };
            interstitial.OnAdFailedToLoad += (sender, args) =>
            {
                PrintStatus("Interstitial ad failed to load with error: " + args.LoadAdError.GetMessage());
                OnAdCallback(AdsEventType.InterstitialFailedToLoad, adMediation);
            };
            interstitial.OnAdOpening += (sender, args) =>
            {
                PrintStatus("Interstitial ad opening.");
            };
            interstitial.OnAdClosed += (sender, args) =>
            {
                PrintStatus("Interstitial ad closed.");
                OnAdCallback(AdsEventType.InterstitialClosed, adMediation);
            };
            interstitial.OnAdDidRecordImpression += (sender, args) =>
            {
                PrintStatus("Interstitial ad recorded an impression.");
            };
            interstitial.OnAdFailedToShow += (sender, args) =>
            {
                PrintStatus("Interstitial ad failed to show.");
            };
            interstitial.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}", "Interstitial ad received a paid event.",
                                            args.AdValue.CurrencyCode, args.AdValue.Value);
                Tracking.Instance.AbmobInterstialAdsPaidEvent(adUnitId, args);
                PrintStatus(msg);
            };


            // Load the interstitial with the request.
            interstitial.LoadAd(CreateAdRequest());
        }

        public void LoadRewardedAd(ref RewardedAd rewardedAd, string adUnitId, Action<AdsEventType, AdMediationFramework> OnAdCallback, AdMediationFramework adMediation)
        {
            rewardedAd = new RewardedAd(adUnitId);

            // Add Event Handlers
            rewardedAd.OnAdLoaded += (sender, args) =>
            {
                PrintStatus("Reward ad loaded.");
                OnAdCallback(AdsEventType.RewardAdLoaded, adMediation);
            };
            rewardedAd.OnAdFailedToLoad += (sender, args) =>
            {
                PrintStatus("Reward ad failed to load.");
                OnAdCallback(AdsEventType.RewardAdFailedToLoad, adMediation);
            };
            rewardedAd.OnAdOpening += (sender, args) =>
            {
                PrintStatus("Reward ad opening.");
            };
            rewardedAd.OnAdFailedToShow += (sender, args) =>
            {
                PrintStatus("Reward ad failed to show with error: " + args.AdError.GetMessage());
            };
            rewardedAd.OnAdClosed += (sender, args) =>
            {
                PrintStatus("Reward ad closed.");
                OnAdCallback(AdsEventType.RewardAdClosed, adMediation);

            };
            rewardedAd.OnUserEarnedReward += (sender, args) =>
            {
                PrintStatus("User earned Reward ad reward: " + args.Amount);
                OnAdCallback(AdsEventType.RewardAdEarned, adMediation);
            };
            rewardedAd.OnAdDidRecordImpression += (sender, args) =>
            {
                PrintStatus("Reward ad recorded an impression.");
            };
            rewardedAd.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}", "Rewarded ad received a paid event.",
                                            args.AdValue.CurrencyCode, args.AdValue.Value);
                Tracking.Instance.AbmobRewardAdsPaidEvent(adUnitId, args);
                PrintStatus(msg);
            };

            // Load the rewarded ad with the request.
            rewardedAd.LoadAd(CreateAdRequest());
        }

        // Returns an ad request with custom ad targeting.
        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder().Build();
        }

        private void PrintStatus(string message)
        {
            Debug.Log(message);
        }
    }
}
#endif