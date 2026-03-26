using Gemmob.Api.Analytics;
using Gemmob;

public partial class Tracking {
    public enum AdsType : byte { Interstitial, RewardVideo }

    #region LogAds
    //public void LogAds(string position, AdsType type) {
    //    switch (type) {
    //        case AdsType.Interstitial:
    //            LogEvent($"inter_{position}", ParameterBuilder.Create());
    //            break;
    //        case AdsType.RewardVideo:
    //            LogEvent($"reward_{position}", ParameterBuilder.Create());
    //            break;
    //        default:
    //            LogEvent("ads", ParameterBuilder.Create()
    //                                            .Add("position", position)
    //                                            .Add("type", (byte)type));
    //            break;
    //    }

    //    Logs.Log($"Tracking_ads: position = {position}, position = {type}");
    //}
    #endregion

    #region User Properties
    //public void SetProperty(TrackingUserProperty type, string properties) {
    //    switch (type) {
    //        case TrackingUserProperty.CurrentProgress:
    //            SetCurrentProgressProperty(properties);
    //            break;
    //        case TrackingUserProperty.CurrentUpgrade:
    //            SetCurrentUpgradeProperty(properties);
    //            break;
    //        default:
    //            break;
    //    }
    //}
    //private void SetCurrentProgressProperty(string properties) {
    //    SetUserProperty("current_progress", properties);
    //    Logs.Log($"Tracking_current_progress: {properties}");
    //}
    //private void SetCurrentUpgradeProperty(string properties) {
    //    SetUserProperty("current_upgrade", properties);
    //    Logs.Log($"Tracking_current_upgrade: {properties}");
    //}
    #endregion

    #region Currency
    //public void LogCurrencyEarn(string type, string amount, string screenID) {
    //    LogEvent("currency_earn", ParameterBuilder.Create()
    //                                            .Add("type", type)
    //                                            .Add("amount", amount)
    //                                            .Add("screen", screenID));
    //    Logs.Log($"Tracking_currency_earn: type = {type}, amount = {amount}, screen = {screenID}");
    //}
    //public void LogCurrencySpend(string type, string amount, string screenID) {
    //    LogEvent("currency_spend", ParameterBuilder.Create()
    //                                            .Add("type", type)
    //                                            .Add("amount", amount)
    //                                            .Add("screen", screenID));
    //    Logs.Log($"Tracking_currency_spend: type = {type}, amount = {amount}, screen = {screenID}");
    //}
    #endregion

    #region StartLevel, FinishLevel
    //public void LogStartLevel(string zoneID, string drone1ID, string drone2ID, string weaponryID, string shieldID, string reactorID, string propulsion) {
    //    LogEvent("level_start_" + zoneID, ParameterBuilder.Create()
    //                                    .Add("zone", zoneID)
    //                                    .Add("drone1", drone1ID)
    //                                    .Add("drone2", drone2ID)
    //                                    .Add("weaponry", weaponryID)
    //                                    .Add("shield", shieldID)
    //                                    .Add("reactor", reactorID)
    //                                    .Add("propulsion", propulsion));

    //    Logs.Log($"Tracking_level_start: zone= {zoneID}, drone1 ={drone1ID}, drone2 ={drone2ID}, weaponry ={weaponryID}, shield ={shieldID}, reactor ={reactorID}, propulsion ={propulsion}");
    //}

    //public void LogTutorialStartWave(string wave) {
    //    LogEvent($"tutorial_start_w{wave}", ParameterBuilder.Create()
    //                                    .Add("wave", wave));
    //    Logs.Log($"Tracking_tutorial_start: wave = {wave}");
    //}

    //public void LogTutorialEndLevel(string level, string result) {
    //    LogEvent($"tutorial_finish_w{level}", ParameterBuilder.Create()
    //                                    .Add("result", result));
    //    Logs.Log($"Tracking_tutorial_finish: wave = {level},result= {result}");
    //}
    //public void LogStartWave(string zoneID, string wave) {
    //    LogEvent($"level_start_z{zoneID}w{wave}", ParameterBuilder.Create()
    //                                    .Add("wave", wave));
    //}

    //public void LogEndLevel(string level, string result, string bullet, string mods, string zoneID, string drone1ID, string drone2ID, string weaponryID, string shieldID, string reactorID, string propulsion, string firstTime, string firstLose) {
    //    LogEvent($"level_finish_z{zoneID}w{level}", ParameterBuilder.Create()
    //                                    .Add("level", level)
    //                                    .Add("result", result)
    //                                    .Add("bullet", bullet)
    //                                    .Add("mods", mods)
    //                                    .Add("zone", zoneID)
    //                                    .Add("drone1", drone1ID)
    //                                    .Add("drone2", drone2ID)
    //                                    .Add("weaponry", weaponryID)
    //                                    .Add("shield", shieldID)
    //                                    .Add("reactor", reactorID)
    //                                    .Add("propulsion", propulsion)
    //                                    .Add("firstTime", firstTime));

    //    if (firstLose.Equals("1"))
    //        LogEvent($"first_lose ", ParameterBuilder.Create()
    //                                                 .Add("zone", zoneID)
    //                                                 .Add("level", level));
    //    Logs.Log($"Tracking_level_end: level= {level},result= {result},bullet= {bullet},mods= {mods},zone= {zoneID}, drone1 ={drone1ID}, drone2 ={drone2ID}, weaponry ={weaponryID}, shield ={shieldID}, reactor ={reactorID}, propulsion ={propulsion}, firstTime ={firstTime}, firstLose ={firstTime}");
    //}
    #endregion

    #region OldTracking
    //public void TrackingWhenLose() {
    //    TrackingUserWhenLose();
    //    TrackingEventWhenLose();
    //}
    //public void TrackingWhenWin() {
    //    TrackingUserWhenWin();
    //    TrackingEventWhenWin();
    //}
    //private void TrackingUserWhenLose() {
    //    string result = $"{GameResources.Instance.ConquerorData.CurrentZoneIndex}.{GameResources.Instance.ConquerorData.CurrentZone.CurrentWave}";
    //    SetProperty(TrackingUserProperty.CurrentProgress, result);
    //}
    //private void TrackingUserWhenWin() {
    //    string result = $"{GameResources.Instance.ConquerorData.CurrentZoneIndex}.{GameResources.Instance.ConquerorData.CurrentZone.CurrentWave}";
    //    SetProperty(TrackingUserProperty.CurrentProgress, result);
    //}
    //private void TrackingEventWhenLose() {

    //}
    //private void TrackingEventWhenWin() {

    //}
    //public void TrackingOnEnhanceShip() {
    //    string result = "";
    //    var data = GameResources.Instance.Ship.Datas;
    //    foreach (var item in data) {
    //        result += $"{item.Name}: {item.CurrentLevel}, ";
    //    }
    //    SetProperty(TrackingUserProperty.CurrentUpgrade, result);
    //}

    //public void TrackingIapItemClicked(string itemName) {
    //    GameSystem.Common.UI.Frame screen = GameSystem.Common.UI.HUDManager.GetFrameOnTopAllHUD();
    //    if (screen != null) {
    //        LogEvent($"{screen.ScreenName}_{CompactKey(itemName)}_click", ParameterBuilder.Create());
    //    }
    //}

    //public void TrackingPurchaseSuccessed(string itemName) {
    //    GameSystem.Common.UI.Frame screen = GameSystem.Common.UI.HUDManager.GetFrameOnTopAllHUD();
    //    if (screen != null) {
    //        LogEvent($"{screen.ScreenName}_{CompactKey(itemName)}_success", ParameterBuilder.Create());
    //    }
    //}

    //public void TrackingPurchaseFaid(string itemName) {
    //    GameSystem.Common.UI.Frame screen = GameSystem.Common.UI.HUDManager.GetFrameOnTopAllHUD();
    //    if (screen != null) {
    //        LogEvent($"{screen.ScreenName}_{CompactKey(itemName)}_fail", ParameterBuilder.Create());
    //    }
    //}
#endregion


    public string CompactKey(string key) {
        return key.Replace("com.shootero.", "");
    }
    #region First Win
    public void LogFirstWinZone(int time) {
        var conqueror = GameResources.Instance.ConquerorData;
        if (conqueror.CurrentZone.IsTracked)
            return;
        int zone = IngameData.currentGameMode == GameMode.Conqueror ? conqueror.CurrentZoneIndex : -1;
        int revived = GameManager.Instance.ReviveTime;
        int win_times = conqueror.CurrentZone.NumberPlayBeforeFirstWin;
        int shipId = GameResources.Instance.Ship.CurrentShip;
#if !UNITY_EDITOR
        LogEvent("lose", ParameterBuilder.Create()
                                         .Add("zone", zone)
                                         .Add("revived", revived)
                                         .Add("win_times", win_times)
                                         .Add("playtime_win", time)
                                         .Add("ship", shipId));
#else
        Logs.Log($"lose: zone = {zone}, revived = {revived}, win_times = {win_times}, playtime_win = {time}, ship = {shipId}");
#endif
    }
    #endregion

    #region Lose
    public void LogLose(int time) {
        var conqueror = GameResources.Instance.ConquerorData;
        int zone = IngameData.currentGameMode == GameMode.Conqueror? conqueror.CurrentZoneIndex : -1;
        int wave = conqueror.CurrentZone.CurrentWave;
        int shipId = GameResources.Instance.Ship.CurrentShip;
#if !UNITY_EDITOR
        LogEvent("lose", ParameterBuilder.Create()
                                         .Add("zone", zone)
                                         .Add("wave", wave)
                                         .Add("playtime_lose", time)
                                         .Add("ship", shipId));
#else
        Logs.Log($"lose: zone = {zone}, wave = {wave}, playtime_lose = {time}, ship = {shipId}");
#endif
    }
    #endregion

#region Currency
    public void TrackingCurrency(int id, int amount) {
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce)
            return;
        if (amount == 0)
            return;
        GameSystem.Common.UI.Frame screen = GameSystem.Common.UI.HUDManager.GetFrameOnTopAllHUD();
        if (screen == null || screen.ScreenID.Equals("10")) {
            return;
        } else {
            LogCurrency(id, amount, PrefSaver.ButtonKey);
        }
    }

    public void LogCurrency(int id, int amount, string buttonKey) {
        string currencyType = id == 0 ? "chip" : "gem";
#if !UNITY_EDITOR
        LogEvent("currency", ParameterBuilder.Create()
                                             .Add("change", amount)
                                             .Add("type", currencyType)
                                             .Add("action", amount > 0 ? 0 : 1)
                                             .Add("button", buttonKey));
#else
        Logs.Log($"Tracking_currency: type = {currencyType}, amount = {amount}, button = {buttonKey}");
#endif
    }
#endregion

#region Ship
    public void LogShip(string shipId, int level) {
        string shipKey = $"tracking_ship_{shipId}";
        PrefSaver.SetShipQuantity(shipKey);
#if !UNITY_EDITOR
        LogEvent("ship", ParameterBuilder.Create()
                                               .Add("id", shipId)
                                               .Add("level", PrefSaver.GetShipQuantity(shipKey)));
#else
        Logs.Log($"ship: id = {shipKey}, level = {PrefSaver.GetShipQuantity(shipKey)}");
#endif
    }
#endregion

#region Shop

    public void LogShop(ShopButton button) {
        string buttonKey = button.ToString();
        PrefSaver.SetShopButtonQuantity(buttonKey);
#if !UNITY_EDITOR
        LogEvent("shop", ParameterBuilder.Create()
                                               .Add("string", buttonKey)
                                               .Add("quantity", PrefSaver.GetShopButtonQuantity(buttonKey)));
#else
        Logs.Log($"shop: string = {buttonKey}, quantity = {PrefSaver.GetShopButtonQuantity(buttonKey)}");
#endif
    }
#endregion

#region Mode
    public void LogMode(GameMode mode) {
        string modeKey = mode.ToString();
        PrefSaver.SetModeQuantity(modeKey);
#if !UNITY_EDITOR
        LogEvent("mode", ParameterBuilder.Create()
                                               .Add("mode", modeKey)
                                               .Add("quantity", PrefSaver.GetModeQuantity(modeKey)));
#else
        Logs.Log($"mode: mode = {modeKey}, quantity = {PrefSaver.GetModeQuantity(modeKey)}");
#endif
    }
    #endregion

    #region InAppPurchase

    public void LogIap(string position) {
        PrefSaver.SetIapQuantity(CompactKey(position));
#if !UNITY_EDITOR
        LogRevenueData(position);
        LogEvent("inapp", ParameterBuilder.Create()
                                               .Add("string", position)
                                               .Add("quantity", PrefSaver.GetIapQuantity(position)));
#else
        Logs.Log($"inapp: string = {position}, quantity = {PrefSaver.GetIapQuantity(position)}");
#endif
    }

    private void LogRevenueData(string position) {
        var pack = GameIAP.Instance.GetLocalPrice(position);
        string IsoCurrencyCode = pack.isoCurrencyCode;
        decimal CurrentLocalizedPrice = pack.localizedPrice;
        LogEvent("af_revenue", ParameterBuilder.Create(AFInAppEvents.CONTENT_ID, position)
                .Add(AFInAppEvents.REVENUE, CurrentLocalizedPrice)
                .Add(AFInAppEvents.CURRENCY, IsoCurrencyCode));
    }
#endregion

#region RewardAds
    public void LogRewardAds(RewardAdsPos position) {
        PrefSaver.SetRewardAdsQuantity(position);
#if !UNITY_EDITOR
        LogEvent("reward_ads", ParameterBuilder.Create()
                                               .Add("string", position)
                                               .Add("quantity", PrefSaver.GetRewardAdsQuantity(position)));
#else
        Logs.Log($"reward_ads: string = {position}, quantity = {PrefSaver.GetRewardAdsQuantity(position)}");
#endif
    }

    public void AbmobInterstialAdsPaidEvent(string unitId, GoogleMobileAds.Api.AdValueEventArgs args) {
        var adValue = args.AdValue;
        Logs.Log( $"HandleAdPaidEvent received with ad value (in micros): {adValue.Value}, precision: {adValue.Precision}, currency:{adValue.CurrencyCode} from ad network adapter admob");
        Firebase.Analytics.Parameter[] ltvParameters =
        {
            new Firebase.Analytics.Parameter("valuemicros", adValue.Value),
            new Firebase.Analytics.Parameter("value", adValue.Value / 1000000f),
            new Firebase.Analytics.Parameter("currency", adValue.CurrencyCode),
            new Firebase.Analytics.Parameter("precision", (int)adValue.Precision),
            new Firebase.Analytics.Parameter("adunitid", unitId),
            new Firebase.Analytics.Parameter("network", "admob")
        };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Ad_Impression_Revenue", ltvParameters);
    }
    public void AbmobRewardAdsPaidEvent(string unitId, GoogleMobileAds.Api.AdValueEventArgs args) {
        var adValue = args.AdValue;
        Logs.Log( $"HandleAdPaidEvent received with ad value (in micros): {adValue.Value}, precision: {adValue.Precision}, currency:{adValue.CurrencyCode} from ad network adapter admob");
        Firebase.Analytics.Parameter[] ltvParameters =
        {
            new Firebase.Analytics.Parameter("valuemicros", adValue.Value),
            new Firebase.Analytics.Parameter("value", adValue.Value / 1000000f),
            new Firebase.Analytics.Parameter("currency", adValue.CurrencyCode),
            new Firebase.Analytics.Parameter("precision", (int)adValue.Precision),
            new Firebase.Analytics.Parameter("adunitid", unitId),
            new Firebase.Analytics.Parameter("network", "admob")
        };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Ad_Impression_Revenue", ltvParameters);
    }
#endregion
}


public enum RewardAdsPos {
    chest_normal_ads,
    chest_elite_ads,
    daily_free_pack_x3,
    reroll_ads,
    supply_pack,
    revive_ads,
    afk_x2,
    energy_ads,
    ads_spin,
    full_heal,
    ability_ads,
    skip_challenge,
    daily_packs,
    rookie_ads,
    test_ads,
}
public enum ShopButton {
    chest_normal_gem,
    chest_normal_key,
    chest_elite_gem,
    chest_elite_key,
    chest_elite_10,
    chest_skill,
    chip_pack2,
    chip_pack3,
    reroll_2,
    reroll_3,
    daily_free_pack,
    test,
    shop_reward_ads,
}