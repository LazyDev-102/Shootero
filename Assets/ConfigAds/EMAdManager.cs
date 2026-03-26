using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Gemmob;
using System;
using TMPro;
using Gemmob.Lib.Adsv2;

public class EMAdManager : SingletonBind<EMAdManager> {

    //[SerializeField] private EMAdmobManager admob01;
    //[SerializeField] private EMAdmobManager admob02;
    //[SerializeField] private EMAdmobManager admob03;
    //[SerializeField] private EMApplovinManager appLovin;
    //[SerializeField] private EMFanManager fan;
    //[SerializeField] private EMUnityAdsManager unityAds;

    //[SerializeField] private EMAdRemote eMAdRemote;

    //private List<EMAdService> adServiceUsable = new List<EMAdService>();

    [SerializeField]
    private AdsManager adsManager;

    private Action onSuccessed;
    private Action onFailed;

    private bool Available => Networks.IsInternetAvaiable;
    //private bool isRequesting;
    private bool isRewardVideo;

    protected override void OnAwake() {
        base.OnAwake();
        //RuntimeManager.Init();
    }
    private void Start() {
        if (Available && gameObject.activeInHierarchy) {
            StartCoroutine(InitAdServive());
        }
    }


    public bool HasRewardAds() {
        
        return adsManager.IsRewardBasedVideoLoaded();
    }

    private IEnumerator InitAdServive() {
        //yield return new WaitUntil(Advertising.IsInitialized);
        //eMAdRemote.AddActionOnLoad(RegisterAdService);
        //yield return Yielder.Wait(1f);
        //eMAdRemote.ReloadDataByRemote();

        //auAdvertising.InterstitialAdCompleted += OnInterstialClosed;
        //Advertising.RewardedAdCompleted += OnRewardedAdCompleted;
        //Advertising.RewardedAdSkipped += RewardedAdSkipped;

        yield return Yielder.Wait(1f);
        adsManager.Init();
    }

    

    [ContextMenu("Show Reward")]
    public void ShowRewardTest() {
        adsManager.ShowRewardBasedVideo(null, null);
    }

    [ContextMenu("Show Inters")]
    public void ShowIntersTest() {
        adsManager.ShowInterstitial();
    }
    [ContextMenu("Show Banner")]
    public void ShowBanner() {
        adsManager.ShowBanner();
    }

    public void ShowRewardAds(RewardAdsPos position, Action onSuccessed, Action onFailed = null) {
        if (Available)
        {
            //AddResult(onSuccessed, onFailed);
            //for (int i = 0; i < adServiceUsable.Count; i++) {
            //    if (adServiceUsable[i].IsRewardAdsReady()) {
            //        isRequesting = true;
            //        adServiceUsable[i].ShowRewardAds();
            //        isRewardVideo = true;
            //        Tracking.Instance.LogRewardAds(position);
            //        return;
            //    }
            //    else {
            //        adServiceUsable[i].RewardInitialize();
            //    }
            //}
            adsManager.ShowRewardBasedVideo(() =>
            {
                EventDispatcher.Instance.Dispatch(EventKey.OnWatchRewardAdSuccess);
                onSuccessed?.Invoke();
            }, onFailed);
        }
        else onFailed?.Invoke();
    }
    public void ShowInterstitialAds(RewardAdsPos position, Action onSuccessed, Action onFailed = null) {
        if (Available) {
            adsManager.ShowInterstitial(onSuccessed);
        }
    }
    public void ShowBanner(BannerAdPosition position) {
        adsManager.ShowBanner();
    }

    [ContextMenu("Hide Banner")]
    public void HideBanner() {
        //Advertising.HideBannerAd();
        adsManager.HideBanner();
    }

    //private void AddResult(Action onSuccessed, Action onFailed) {
    //    this.onSuccessed = onSuccessed;
    //    this.onFailed = onFailed;
    //}

    //private void OnInterstialClosed(InterstitialAdNetwork interstitialAd, AdPlacement adPlacement) {
    //    Debug.LogError($"Close Ads with AdPlacement: {adPlacement.Name} Ads");
        
    //    isRequesting = false;
    //}
    //private void OnRewardedAdCompleted(RewardedAdNetwork rewardedAd, AdPlacement adPlacement) {
    //    Debug.LogError($"Complete Ads with AdPlacement: {adPlacement.Name} Ads");
        
    //    DispatchEvent();
    //    onSuccessed?.Invoke();
    //    onSuccessed = null;
    //    isRequesting = false;
    //}
    //private void RewardedAdSkipped(RewardedAdNetwork rewardedAd, AdPlacement adPlacement) {
    //    Debug.LogError($"Skip Ads with AdPlacement: {adPlacement.Name} Ads");
        
    //    onFailed?.Invoke();
    //    onFailed = null;
    //    isRequesting = false;
    //}

    //private void DispatchEvent() {
    //    if (isRewardVideo)
    //        EventDispatcher.Instance.Dispatch(EventKey.OnWatchRewardAdSuccess);
    //}
    //private void AdmobInterstialOnPaidEvent(AdMobClientImpl.Interstitial.Instance interstitial, object sender, GoogleMobileAds.Api.AdValueEventArgs args) {
    //    Tracking.Instance.AbmobInterstialAdsPaidEvent(interstitial.Placement.Name, args);
    //}

    //private void AdmobRewardOnPaidEvent(AdMobClientImpl.Rewarded.Instance reward, object sender, GoogleMobileAds.Api.AdValueEventArgs args) {
    //    Tracking.Instance.AbmobRewardAdsPaidEvent(reward.Placement.Name, args);
    //}
}
