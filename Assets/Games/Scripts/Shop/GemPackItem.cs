
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GemPackItem", menuName = "Resource/Item/Packs/GemPackItem")]
public class GemPackItem : PackItem {

    public bool IsBought { get; set; }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            IsBought = false;
            return;
        }

        IsBought = saveData.IsBought;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.IsBought = IsBought;
        return JsonUtility.ToJson(saveData);
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private bool ib;

        public bool IsBought { get => ib; set => ib = value; }
    }
}
