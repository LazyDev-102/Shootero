using UnityEngine;

public partial class PrefSaver {

    public static string ButtonKey = "";

    public static int GetRewardAdsQuantity(RewardAdsPos pos) {
        return PlayerPrefs.GetInt(pos.ToString(), 0);
    }
    public static void SetRewardAdsQuantity(RewardAdsPos pos) {
        PlayerPrefs.SetInt(pos.ToString(), GetRewardAdsQuantity(pos)+1);
    }

    public static int GetIapQuantity(string pos) {
        return PlayerPrefs.GetInt(pos, 0);
    }
    public static void SetIapQuantity(string pos) {
        PlayerPrefs.SetInt(pos, GetIapQuantity(pos)+1);
    }

    public static int GetModeQuantity(string mode) {
        return PlayerPrefs.GetInt(mode, 0);
    }
    public static void SetModeQuantity(string mode) {
        PlayerPrefs.SetInt(mode, GetIapQuantity(mode) +1);
    }

    public static int GetShopButtonQuantity(string button) {
        return PlayerPrefs.GetInt(button, 0);
    }
    public static void SetShopButtonQuantity(string button) {
        PlayerPrefs.SetInt(button, GetIapQuantity(button) +1);
    }

    public static int GetShipQuantity(string shipKey) {
        return PlayerPrefs.GetInt(shipKey, 0);
    }
    public static void SetShipQuantity(string shipKey) {
        PlayerPrefs.SetInt(shipKey, GetIapQuantity(shipKey) +1);
    }

}

