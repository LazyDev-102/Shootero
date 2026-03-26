
using Helper;
using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RerollPackItem", menuName = "Resource/Item/Packs/RerollPackItem")]
public class RerollPackItem : Item, ISaveLoadable {
    [SerializeField] private ShopButton shopButtonKey;
    [SerializeField] private bool isFree;
    [SerializeField] private bool watched;
    [SerializeField] private ItemClaim[] itemClaims;

    public ShopButton ShopButtonKey { get => shopButtonKey; }
    public bool IsFree { get => isFree; }
    public bool Watched { get => watched; set => watched = value; }
    public ItemClaim[] ItemClaims { get => itemClaims; }
    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds; }

    public override void Claim(int amount) {
        foreach (var item in itemClaims) {
            item.Claim();
        }
        Save();
    }
    private void Save() {
        if (!isFree)
            return;
        watched = true;
        GameResources.Instance.ShopData.Rerolls.Save(DateTime.Now.DayOfYear, DateTime.Now.Year);
    }
    public void ResetData(bool status) {
        if (status)
            watched = false;
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
        watched = saveData.Watched;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.Watched = watched;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            ResetData(true);
        }
        else {
            watched = json[JsonKey.Watched].AsBool;
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Watched, watched);
        return node;
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private bool watched;
        public bool Watched { get => watched; set => watched = value; }
    }
}
