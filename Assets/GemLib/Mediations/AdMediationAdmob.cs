//https://developers.google.com/admob/unity/quick-start#initialize_the_mobile_ads_sdk
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

#if GEM_ADMOB_NATIVE
namespace Gemmob.Lib.Adsv2
{
    public class AdMediationAdmob : AdMediationFramework
    {
        private BannerView bannerView;
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;

        AdMediationAdmobMed adMediationAdmobMed;

        public AdMediationAdmob(AdMediationAdmobMed adMediationAdmobMed)
        {
            this.adMediationAdmobMed = adMediationAdmobMed;
        }

        public override void Initialization(System.Action<AdsEventType, AdMediationFramework> OnAdCallback, AdsConfig adsConfig)
        {
            base.Initialization(OnAdCallback, adsConfig);
            OnAdCallback(AdsEventType.OnSDKInitialized, this);
        }

        public override void RequestBanner()
        {
            string adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ?
                adsConfig.admobConfig.bannerUnitIdIos : adsConfig.admobConfig.bannerUnitIdAndroid;
            adMediationAdmobMed.LoadBanner(ref this.bannerView, adUnitId, OnAdCallback, this);
        }

        public override void RequestInterstitial()
        {
            string adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ?
                adsConfig.admobConfig.interstitialUnitIdIos : adsConfig.admobConfig.interstitialUnitIdAndroid;
            adMediationAdmobMed.LoadInterstitial(ref this.interstitial, adUnitId, OnAdCallback, this);
        }

        public override void RequestRewardBasedVideo()
        {
            string adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ?
                adsConfig.admobConfig.rewardAdUnitIdIos : adsConfig.admobConfig.rewardAdUnitIdAndroid;
            adMediationAdmobMed.LoadRewardedAd(ref rewardedAd, adUnitId, OnAdCallback, this);
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

        public override void ShowMediationTestSuite() 
        {
            adMediationAdmobMed.ShowMediationTestSuite();
        }
    }
}
#endif