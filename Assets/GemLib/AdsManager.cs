using System;
using System.Collections.Generic;
using UnityEngine;
using Gemmob.Lib.Tools;

namespace Gemmob.Lib.Adsv2
{
    public class AdsManager : MonoBehaviour
    {
        public static AdsManager instance;
        private Action OnRewardVideoCompleted;
        private Action OnRewardVideoIncompleted;
        private Action OnInterstitialClose;
        [HideInInspector]
        public RewardAdContext rewardAdContext;

        //todo: should use anti-cheat save method
        public bool IsAdsRemoved
        {
            set { PlayerPrefs.SetInt("AdsRemoved", value ? 1 : 0); }
            get { return PlayerPrefs.GetInt("AdsRemoved", 0) == 0 ? false : true; }
        }

        //todo: should use anti-cheat save method
        public long NowUnixTimestamp()
        {
            return System.DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        //todo: should use anti-cheat save method
        public long LastInterstitialShownTime
        {
            set { PlayerPrefs.SetString("LastInterstitialShown", value.ToString()); }
            get { return long.Parse(PlayerPrefs.GetString("LastInterstitialShown", "0")); }
        }


        public void RequestBanner()
        {
            MonoBehaviour.print("-Ad Manager: Request Banner");
            for (int i = adsMediations.Count - 1; i >= 0; i--) 
                if(adsMediations[i].isSDKInitialized) adsMediations[i].RequestBanner();
        }

        public void RequestInterstitial()
        {
            MonoBehaviour.print("-Ad Manager: Request Interstitial");
            for (int i = adsMediations.Count - 1; i >= 0; i--)
                if (adsMediations[i].isSDKInitialized) adsMediations[i].RequestInterstitial();
        }

        public void RequestRewardBasedVideo()
        {
            MonoBehaviour.print("-Ad Manager: Request RewardBasedVideo");
            for (int i = adsMediations.Count - 1; i >= 0; i--)
                if (adsMediations[i].isSDKInitialized) adsMediations[i].RequestRewardBasedVideo();
        }

        public void ShowInterstitial(Action OnInterstitialClose = null)
        {
            this.OnInterstitialClose = OnInterstitialClose;
            for(int i = 0; i< adsMediations.Count; i++)
                if (adsMediations[i].IsInterstitialLoaded())
                {
                    adsMediations[i].ShowInterstitial();
                    return;
                }
            MonoBehaviour.print("-Ad Manager: Interstitial is not ready yet");
        }

        public void ShowRewardBasedVideo(Action OnRewardVideoCompleted, Action OnRewardVideoIncompleted = null, 
            RewardAdContext rewardAdContext = RewardAdContext.Unknown)
        {
            rewardBasedVideoCompleted = false;
            this.OnRewardVideoCompleted = OnRewardVideoCompleted;
            this.OnRewardVideoIncompleted = OnRewardVideoIncompleted;
            this.rewardAdContext = rewardAdContext;

            for (int i = 0; i < adsMediations.Count; i++)
                if (adsMediations[i].IsRewardBasedVideoLoaded())
                {
                    adsMediations[i].ShowRewardBasedVideo();
                    return;
                }
            MonoBehaviour.print("-Ad Manager: Reward based video ad is not ready yet");
        }

        public bool IsInterstitialLoaded(Action OnInterstitialClose)
        {
            this.OnInterstitialClose = OnInterstitialClose;
            if (IsAdsRemoved || NowUnixTimestamp() - LastInterstitialShownTime < durationBtwInterstitialInSec) return false;
#if !UNITY_EDITOR
            for (int i = 0; i < adsMediations.Count; i++)
                if (adsMediations[i].IsInterstitialLoaded()) return true;
            return false;
#else
            return true;
#endif
        }

        public bool IsRewardBasedVideoLoaded()
        {
#if !UNITY_EDITOR
            for (int i = 0; i < adsMediations.Count; i++)
                if (adsMediations[i].IsRewardBasedVideoLoaded()) return true;
            return false;
#endif
            return true;
        }

        public void DestroyBanner()
        {
            adsMediationShowingBanner.DestroyBanner();
        }

        public void HideBanner()
        {
            if (adsMediationShowingBanner != null) adsMediationShowingBanner.HideBanner();
            else
                for (int i = 0; i < adsMediations.Count; i++) adsMediations[i].HideBanner();
        }

        public void ShowBanner()
        {
            for (int i = 0; i < adsMediations.Count; i++)
                if (adsMediations[i].IsBannerLoaded())
                {
                    adsMediations[i].ShowBanner();
                    adsMediationShowingBanner = adsMediations[i];
                    return;
                }
            MonoBehaviour.print("-Ad Manager: Banner is not ready yet");
        }

        public void ShowMediationTestSuite(int index)
        {
            adsMediations[index].ShowMediationTestSuite();
        }   

        public void ShowMediationTestSuite()
        {
            isShowingMediationTestSuite = true;
        }
#if TEST
        private void OnGUI()
        {
            if (!isShowingMediationTestSuite) return;

            int width = Screen.width -50, height = Screen.height -50;
            GUI.BeginGroup(new Rect(Screen.width / 2 - width/2, Screen.height / 2 - height /2, width, height));
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            GUIStyle myGUIStyle = new GUIStyle(GUI.skin.button);
            myGUIStyle.fontSize = 26;
            GUI.Box(new Rect(0, 0, width, height), "");
            //if( GUI.Button(new Rect(10, 40, 300, 80), "Click me", myGUIStyle))
            //{
            //    Debug.LogError("hello");
            //}

            int btnWidth = width - 100, btnHeight = 60, y = 200;
            for(int i = 0; i < adsMediations.Count; i++)
            {
                if (useBanner)
                {
                    if (GUI.Button(new Rect(50, y, btnWidth, btnHeight),
                        adsMediations[i].GetType().Name + (adsMediations[i].IsBannerLoaded() ? " banner loaded" : " banner not ready"), myGUIStyle))
                        if (adsMediations[i].IsBannerLoaded())
                        {
                            adsMediations[i].ShowBanner(); 
                            isShowingMediationTestSuite = false;
                        }
                    y += btnHeight + 10;
                }
                if (useInterstitial)
                {
                    if (GUI.Button(new Rect(50, y, btnWidth, btnHeight),
                        adsMediations[i].GetType().Name + (adsMediations[i].IsInterstitialLoaded() ? " interstitial loaded" : " interstitial not ready"), myGUIStyle))
                        if (adsMediations[i].IsInterstitialLoaded())
                        {
                            adsMediations[i].ShowInterstitial();
                            isShowingMediationTestSuite = false;
                        }
                    y += btnHeight + 10;
                }

                if (GUI.Button(new Rect(50, y, btnWidth, btnHeight),
                    adsMediations[i].GetType().Name + (adsMediations[i].IsRewardBasedVideoLoaded() ? " rewardAd loaded" : " rewardAd not ready"), myGUIStyle))
                    if (adsMediations[i].IsRewardBasedVideoLoaded())
                    {
                        adsMediations[i].ShowRewardBasedVideo();
                        isShowingMediationTestSuite = false;
                    }
                y += btnHeight + 10;

                if (GUI.Button(new Rect(50, y, btnWidth, btnHeight), "Open Test Suite", myGUIStyle))
                {
                    adsMediations[i].ShowMediationTestSuite();
                    isShowingMediationTestSuite = false;
                }
                y += btnHeight + 10;

                y += 30;
            }

            if (GUI.Button(new Rect(55 + btnWidth, 20, 40, 40), "X", myGUIStyle)) isShowingMediationTestSuite = false;
            GUI.EndGroup();
        }
#endif


        /// ///////////////////////////////////////////////////////////////////////////////////////

#region do not touch
        private List<AdMediationFramework> adsMediations;
        private AdMediationFramework adsMediationShowingBanner;
        private int durationBtwInterstitialInSec = 10;
        private bool rewardBasedVideoCompleted = false;
        private float[] delayRetryRequestAds = { 2f, 60, 300, 600, 1200 };
        private List<RetryQueueElement> retryQueue;
        private bool isShowingMediationTestSuite = false;

        [SerializeField]
        [Header("Ad formats")]
        private bool useBanner = false;
        [SerializeField]
        private bool useInterstitial = false, useRewardAd = true;
        [SerializeField]
        [Header("Auto mode")]
        [Space(5)]
        private bool autoAdLoadingMode = true;
        private bool autoAdStartedFlag = false;


        [SerializeField]
        [Header("Ad platform")]
        private bool useAdmobMediation = true;
        [SerializeField]
        private bool useAdmobNative, useUnityAds;
        [SerializeField]
        [InspectorButton("OnApplyAdPlatform")]
        private bool applyAdPlatform;

        public static bool IsInternetAvaiable => Application.internetReachability != NetworkReachability.NotReachable;

        public void Start()
        {
            if (instance != null && instance != this) Destroy(this);
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            Init();
        }

        public void Init()
        {
            retryQueue = new List<RetryQueueElement>();
            adsMediations = new List<AdMediationFramework>();
            AdsConfig adsConfig = new AdsConfig();

#if GEM_ADMOB_MED || GEM_ADMOB_NATIVE
            AdMediationAdmobMed admobMediation = new AdMediationAdmobMed();
#endif
#if GEM_ADMOB_MED
            adsMediations.Add(admobMediation);
#endif
#if GEM_ADMOB_NATIVE
            adsMediations.Add(new AdMediationAdmob(admobMediation));
#endif
#if GEM_UNITY_AD
            adsMediations.Add(new AdMediationUnity());
#endif

            for (int i = 0; i < adsMediations.Count; i++) adsMediations[i].Initialization(OnAdCallback, adsConfig);
        }

        //Called when apply adPlatform on inspector
        private void OnApplyAdPlatform()
        {
#if UNITY_EDITOR
            List<string> addedSymbols = new List<string>();
            List<string> removedSymbols = new List<string>();
            if (useAdmobMediation) addedSymbols.Add("GEM_ADMOB_MED"); else removedSymbols.Add("GEM_ADMOB_MED");
            if (useAdmobNative) addedSymbols.Add("GEM_ADMOB_NATIVE"); else removedSymbols.Add("GEM_ADMOB_NATIVE");
            if (useUnityAds) addedSymbols.Add("GEM_UNITY_AD"); else removedSymbols.Add("GEM_UNITY_AD");
            GlobalDefineManager.UpdateDefineSymbols(addedSymbols, removedSymbols);
#endif
        }

        public void Update()
        {
            if(retryQueue.Count > 0)
            {
                for(int i = retryQueue.Count-1; i>=0; i--)
                {
                    retryQueue[i].countdownRetryRequestAd -= Time.deltaTime;
                    if(retryQueue[i].countdownRetryRequestAd <= 0)
                    {
                        retryQueue[i].Callback();
                        retryQueue.RemoveAt(i);
                    }
                }
            }
        }


        public void OnAdCallback(AdsEventType adsEventType, AdMediationFramework adMediation)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(HandleCallback, new AdCallbackArgs(adsEventType, adMediation));
        }

        private System.Collections.IEnumerator RunAutoAdRequest()
        {
            autoAdStartedFlag = true;
            yield return new WaitForSeconds(3);
            if (!IsInternetAvaiable) yield return new WaitForSeconds(10);
            if (useBanner) RequestBanner();
            if (useInterstitial) RequestInterstitial();
            if (useRewardAd) RequestRewardBasedVideo();
        }

        private void HandleCallback(AdCallbackArgs arg)
        {
            var adMediation = arg.adMediation;
            switch (arg.adsEventType)
            {
                case AdsEventType.OnSDKInitialized:
                    adMediation.isSDKInitialized = true;
                    Debug.Log("---Ad Manager Callback: inited");
                    if (autoAdLoadingMode && !autoAdStartedFlag) StartCoroutine(RunAutoAdRequest());
                    break;

                case AdsEventType.BannerFailedToLoad:
                    if (adMediation.bannerRetryIndex < delayRetryRequestAds.Length)
                    {
                        retryQueue.Add(new RetryQueueElement(delayRetryRequestAds[adMediation.bannerRetryIndex], adMediation.RequestBanner));
                        adMediation.bannerRetryIndex++;
                    }
                    break;
                case AdsEventType.InterstitialFailedToLoad:
                    if (adMediation.interstitialRetryIndex < delayRetryRequestAds.Length)
                    {
                        retryQueue.Add(new RetryQueueElement(delayRetryRequestAds[adMediation.interstitialRetryIndex], adMediation.RequestInterstitial));
                        adMediation.interstitialRetryIndex++;
                    }
                    break;
                case AdsEventType.RewardAdFailedToLoad:
                    if (adMediation.rewardAdRetryIndex < delayRetryRequestAds.Length)
                    {
                        retryQueue.Add(new RetryQueueElement(delayRetryRequestAds[adMediation.rewardAdRetryIndex], adMediation.RequestRewardBasedVideo));
                        adMediation.rewardAdRetryIndex++;
                    }
                    break;

                case AdsEventType.BannerLoaded:
                    adMediation.bannerRetryIndex = 0;
                    Debug.Log("---Ad Manager Callback: banner loaded");
                    break;
                case AdsEventType.InterstitialLoaded:
                    adMediation.interstitialRetryIndex = 0;
                    Debug.Log("---Ad Manager Callback: Interstitial Loaded");
                    break;
                case AdsEventType.RewardAdLoaded:
                    adMediation.rewardAdRetryIndex = 0;
                    Debug.Log("---Ad Manager Callback: rewardAd Loaded");
                    break;

                case AdsEventType.InterstitialClosed:
                    OnInterstitialClose?.Invoke();
                    OnInterstitialClose = null;
                    Debug.Log("---Ad Manager Callback: interstital closed");
                    adMediation.RequestInterstitial(); // Load another ad
                    break;
                case AdsEventType.RewardAdEarned:
                    rewardBasedVideoCompleted = true;
                    Debug.Log("---Ad Manager Callback: rewardAd: reward   EARNED!");
                    break;
                case AdsEventType.RewardAdClosed:
                    //in some ad network, RewardAdEarned event maybe called after the rewardAdClosed event; hence, execute stuffs after one frame
                    StartCoroutine(RunAfterOneFrame(() =>
                    {
                        if (rewardBasedVideoCompleted) OnRewardVideoCompleted?.Invoke();
                        else OnRewardVideoIncompleted?.Invoke();
                        OnRewardVideoCompleted = null;
                        OnRewardVideoIncompleted = null;
                        Debug.Log("---Ad Manager Callback: reward ad closed; " + (rewardBasedVideoCompleted ? " revoke OnSuccess" : " revoke OnFailed"));
                        rewardBasedVideoCompleted = false;
                        adMediation.RequestRewardBasedVideo();  // Load another ad
                    }));
                    break;
            }
        }


        System.Collections.IEnumerator RunAfterOneFrame(Action action)
        {
            yield return null;
            action();
        }

#endregion

    }

    public enum AdsEventType
    {
        OnSDKInitialized,

        BannerFailedToLoad,
        InterstitialFailedToLoad,
        RewardAdFailedToLoad,

        BannerLoaded,
        InterstitialLoaded,
        RewardAdLoaded,

        InterstitialClosed,
        RewardAdClosed,
        RewardAdEarned, //"User earned Reward ad reward
    }

    public class RetryQueueElement
    {
        public float countdownRetryRequestAd;
        public System.Action Callback;

        public RetryQueueElement(float countdownRetryRequestAd, System.Action Callback)
        {
            this.countdownRetryRequestAd = countdownRetryRequestAd;
            this.Callback = Callback;
        }
    }

    public class AdCallbackArgs
    {
        public AdsEventType adsEventType; 
        public AdMediationFramework adMediation;
        public AdCallbackArgs(AdsEventType adsEventType, AdMediationFramework adMediation)
        {
            this.adsEventType = adsEventType;
            this.adMediation = adMediation;
        }

    }
}