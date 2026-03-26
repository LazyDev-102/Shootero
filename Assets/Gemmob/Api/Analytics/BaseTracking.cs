using Gemmob;
using Gemmob.Api.Analytics;

/**<summary> The main controller of game analytics. (Current use Facebook + Firebase + AppsFlyer + GameAnalytics) </summary>*/
public partial class Tracking : Singleton<Tracking> {

    public override void Preload() {
#if FACEBOOK
        GemFacebookAnalytics.Instance.Preload();
#endif

#if FIREBASE
        GemFirebaseAnalytics.Instance.Preload();
#endif

#if APPSFLYER
        GemAppsFlyerAnalytics.Instance.Preload();
#endif

#if ADJUST
        GemAdjustAnalytics.Instance.Preload();
#endif

#if GA
        GemGameAnalytics.Instance.Preload();
#endif

#if KOCHAVA
        GemKochavaAnalytics.Instance.Preload();
#endif
    }

    /// <summary> Set GA - Game Analytics, This must be call before Preload </summary>
    public void SetGAUserId(string id) {
#if GA
        GemGameAnalytics.SetUserID(id);
#endif
    }

    /// <summary> Currently use for Firebase only. </summary>
    public void SetUserProperty(string name, string properties) {
#if FIREBASE
        GemFirebaseAnalytics.Instance.SetUserProperty(name, properties);
#endif
    }

    public void LogEvent(string eventName, ParameterBuilder builder) {
#if FACEBOOK
        GemFacebookAnalytics.Instance.LogEvent(eventName, builder);
#endif

#if FIREBASE
        GemFirebaseAnalytics.Instance.LogEvent(eventName, builder);
#endif

#if APPSFLYER
        GemAppsFlyerAnalytics.Instance.LogEvent(eventName, builder);
#endif

#if ADJUST
        GemAdjustAnalytics.Instance.LogEvent(eventName, builder);
#endif

#if GA
        GemGameAnalytics.Instance.LogEvent(eventName, builder);
#endif

#if KOCHAVA
        GemKochavaAnalytics.Instance.LogEvent(eventName, builder);
#endif
    }

    public void ExampleLogAds(string position) {
        LogEvent("ads", ParameterBuilder.Create()
                                        .Add("position", position)
                                        .Add("type", "interstitial"));
    }

}