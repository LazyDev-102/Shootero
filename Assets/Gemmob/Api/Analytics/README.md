# CASUAL ANALYTICS API
---------
### *Summary:*
- Package: `CS_Analytics`
- Lastest version: `2.0`
- Namespace: `CS.Api.Analytics`
- Release Date: 17/12/2020

### *Description:*
- This package contains API for using many analytics sponsor: Firebase, Facebook, Appsflyer, Adjust, GameAnalytics (GA), Kochava.

### *SDK References: (Can use more than one) *
- You can get package from this link: http://ip.gemmob.com:1992/share/lib/
- [`FirebaseAnalytics`](https://firebase.google.com/support/release-notes/unity): Firebase SDK package for Unity (>= v7.0.1)
- [`Facebook-unity-sdk`](https://developers.facebook.com/docs/unity/): Facebook SDK package for Unity (>= v8.1.1)
- [`Appsflyer-unity-plugin`](https://github.com/AppsFlyerSDK/appsflyer-unity-plugin/releases): Firebase SDK package for Unity (>= v7.0.1)
- [`Adjust`](https://github.com/adjust/unity_sdk/releases): Adjust SDK package for Unity (>= v4.24.0)
- [`GA-SDK-UNITY`](https://github.com/GameAnalytics/GA-SDK-UNITY/releases): GameAnalytics SDK package for Unity (>= v6.3.7)
- [`Kochava`](https://bintray.com/kochava/Generic/KochavaTracker-Unity): Kochava SDK package for Unity (>= v4.2.1)
--------
# INTERGRATION
---------

### Configurations

- **Initialize:** On the first scene of the Game, create an empty object then add this script `ApiBootstrap` into it, then enable the **preloadAnalytics** field, the script `Tracking` will be initialize automatically. Or an other way you can call `Tracking.Instance.Preload()` to manual init if you don't want to use the Bootstrap.

- **Configuration:** Open Window menu `CS/Api/AnalyticsSettings` then enable the field which the game need to use. Remember to click **Save** button finally.
- **Sponsor Settings**: Open window menu `CS/Api/Analytics/...Settings` then fill all field require such as app id, dev key,...

### Usage
- Use class `Tracking` for manage and control all analytics events in your game. 
- This lib package contains the file `BaseTracking.cs`, which is a partial of class **Tracking**, you should create the other partial to write your custom event, but keep in mind to use base method `LogEvent` to push analytics.
