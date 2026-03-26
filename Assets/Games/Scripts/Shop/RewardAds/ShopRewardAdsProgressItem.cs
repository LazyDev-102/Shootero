
using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopRewardAdsProgressItem", menuName = "Resource/Item/Packs/ShopRewardAdsProgressItem")]
public class ShopRewardAdsProgressItem : Item, ISaveLoadable {
    [SerializeField] private ItemClaim itemClaims;
    [SerializeField] private int target;
    [SerializeField] private bool claimed;

    public bool Claimed { get => claimed; set => claimed = value; }
    public ItemClaim ItemClaims { get => itemClaims; }

    public bool Claimable() {
        if (claimed)
            return false;
        return GameResources.Instance.ShopData.RewardAds.Progress >= target;
    }

    public override void Claim(int amount) {
        itemClaims.Claim();
        claimed = true;
        GameResources.Instance.ShopData.RewardAds.Save(DateTime.Now.DayOfYear, DateTime.Now.Year);
        PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(new ItemClaim[1] { itemClaims });
    }
    public void ResetData(bool status) {
        if (status)
            claimed = false;
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData == null) {
            ResetData(true);
            return;
        }
        claimed = saveData.Claimed;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.Claimed = claimed;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            ResetData(true);
        }
        else {
            claimed = JK.Get(json, JsonKey.Watched, false);
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Watched, claimed);
        return node;
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private bool claimed;
        public bool Claimed { get => claimed; set => claimed = value; }
    }
}
