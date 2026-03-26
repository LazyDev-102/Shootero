
using Helper;
using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopRewardAdsPackItem", menuName = "Resource/Item/Packs/ShopRewardAdsPackItem")]
public class ShopRewardAdsPackItem : Item, ISaveLoadable {
    [SerializeField] private ShopButton shopButtonKey = ShopButton.shop_reward_ads;
    [SerializeField] private int cTurn;
    [SerializeField] private int maxTurn;
    [SerializeField] private bool claimed;
    [SerializeField] private bool rewardConverted;
    [SerializeField] private ItemClaim itemClaims;

    public ShopButton ShopButtonKey { get => shopButtonKey; }
    public ItemClaim ItemClaims { get => itemClaims; }
    public int CTurn { get => cTurn; }
    public int MaxTurn { get => maxTurn; }
    public bool CanWatch => !claimed && cTurn < maxTurn;
    public bool Claimed => claimed;

    public override void Claim(int amount) {
        cTurn++;
        GameResources.Instance.ShopData.RewardAds.Save(DateTime.Now.DayOfYear, DateTime.Now.Year);
        if (!claimed && cTurn >= maxTurn) {
            claimed = true;
            itemClaims.Claim();
            PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(new ItemClaim[1] { itemClaims });
        }
    }

    public float Ratio() {
        return (float)cTurn / (float)maxTurn;
    }

    public void ResetData(bool status) {
        if (status) {
            cTurn = 0;
            claimed = false;
            rewardConverted = false;
        }
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData == null) {
            ResetData(true);
            ConvertReward();
            return;
        }
        cTurn = saveData.CTurn;
        ConvertReward();
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CTurn = cTurn;
        saveData.Claimed = claimed;
        saveData.Converted = rewardConverted;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            ResetData(true);
            ConvertReward();
        }
        else {
            cTurn = JK.Get(json, JsonKey.CurrentRemain, 0);
            claimed = JK.Get(json, JsonKey.IsCompleted, false);
            rewardConverted = JK.Get(json, JsonKey.Converted, false);
            ConvertReward();
        }
    }

    private void ConvertReward() {
        if (rewardConverted)
            return;
        if (itemClaims.Id == ConstantItemID.ChipId) {
            var value = (GameResources.Instance.ChipPerSecond * Constant.HourToSecond * 20).ConvertToInt();
            if (value < 1)
                value = 1;
            itemClaims.Amount = value;
        }
        rewardConverted = true;
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentRemain, cTurn);
        node.Add(JsonKey.IsCompleted, claimed);
        node.Add(JsonKey.Converted, rewardConverted);
        return node;
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private int c;
        [SerializeField] private bool f;
        [SerializeField] private bool v;
        public int CTurn { get => c; set => c = value; }
        public bool Claimed { get => f; set => f = value; }
        public bool Converted { get => v; set => v = value; }
    }
}
