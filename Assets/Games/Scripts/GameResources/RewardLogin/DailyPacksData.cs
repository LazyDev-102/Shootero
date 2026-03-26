using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyPacksData", menuName = "Resource/HardData/Shop/DailyPacksData")]
public class DailyPacksData : ScriptableObject {
    [SerializeField] private DailyPacksInfo[] packs;

    public DailyPacksInfo[] Packs { get => packs; }

    public void Initialize() {
        for (int i = 0; i < packs.Length; i++) {
            packs[i].Initialize();
        }
    }
    public IEnumerable<DailyPacksInfo> GetFreePack() {
        foreach (var item in packs) {
            if (item.IsFree)
                yield return item;
        }
    }
    #region Save Load Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize();
            return;
        }
        if (saveData.PacksInfor == null)
            saveData.PacksInfor = new string[packs.Length];
        var maxLength = saveData.PacksInfor.Length;
        for (int i = 0; i < packs.Length; i++) {
            packs[i].LoadFromJson(i >= maxLength ? null : saveData.PacksInfor[i]);
        }
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.PacksInfor = new string[packs.Length];
        for (int i = 0; i < packs.Length; i++) {
            saveData.PacksInfor[i] = packs[i].SaveToJson();
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONArray json) {
        if (json == null || json.Count <= 0) {
            Initialize();
        }
        else {
            for (int i = 0; i < packs.Length; i++) {
                packs[i].LoadFJson(json[i]);
            }
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONArray();
        for (int i = 0; i < packs.Length; i++) {
            node.Add(packs[i].Save2Json());
        }
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private string[] pi;

        public string[] PacksInfor { get => pi; set => pi = value; }
    }
    #endregion
}