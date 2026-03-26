using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gemmob.Lib.Adsv2
{
    
    public class AdMediationFramework
    {
        public int bannerRetryIndex = 0, interstitialRetryIndex = 0, rewardAdRetryIndex = 0;
        public bool isSDKInitialized = false;
        public System.Action<AdsEventType, AdMediationFramework> OnAdCallback;
        public AdsConfig adsConfig;

        public virtual void Initialization(System.Action<AdsEventType, AdMediationFramework> OnAdCallback, AdsConfig adsConfig)
        {
            this.OnAdCallback = OnAdCallback;
            this.adsConfig = adsConfig;
        }

        //note: in some ads network, request a banner also display the banner right after
        public virtual void RequestBanner() { }

        public virtual void RequestInterstitial() { }

        public virtual void RequestRewardBasedVideo() { }


        public virtual void ShowBanner() { }

        public virtual void ShowInterstitial() { }

        public virtual void ShowRewardBasedVideo() { }


        public virtual bool IsBannerLoaded() { return false;  }
        public virtual bool IsInterstitialLoaded() { return false; }

        public virtual bool IsRewardBasedVideoLoaded() { return false; }

        public virtual void DestroyBanner() { }

        public virtual void HideBanner() { }



        public virtual void ShowMediationTestSuite() { }
    }
}