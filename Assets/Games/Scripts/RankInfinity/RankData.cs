using System;
using UnityEngine;



[CreateAssetMenu(fileName = "RankData", menuName = "Resource/HardData/RankInfinity/RankData")]
public class RankData : ScriptableObject {
    [SerializeField] private int id;
    [SerializeField] private Sprite icon;
    [SerializeField] private string rankName;
    [SerializeField] private MiniRankData[] miniRankDatas;
    [SerializeField] private ItemClaim[] seasonRewards;


    public int Id { get => id; }
    public Sprite Icon { get => icon; }
    public string RankName { get => rankName; }
    public MiniRankData[] MiniRankDatas { get => miniRankDatas; }
    public ItemClaim[] SeasonRewards { get => seasonRewards; }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.MiniRankSave = new string[miniRankDatas.Length];
        for (int i = 0; i < miniRankDatas.Length; ++i) {
            saveData.MiniRankSave[i] = miniRankDatas[i].SaveToJson();
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            for (int i = 0; i < miniRankDatas.Length; ++i) {
                miniRankDatas[i].LoadFromJson(null);
            }
            return;
        }

        int index = 0;
        for (int i = 0; i < saveData.MiniRankSave.Length; ++i) {
            miniRankDatas[i].LoadFromJson(saveData.MiniRankSave[i]);
            index++;
        }
        for (int j = index; j < miniRankDatas.Length; ++j) {
            miniRankDatas[j].LoadFromJson(null);
        }
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private string[] mrs;

        public string[] MiniRankSave { get => mrs; set => mrs = value; }
    }
}


[Serializable]
public class MiniRankData {
    [SerializeField] private string rankName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int rPRequire;
    [SerializeField] private ItemClaim[] instantReward;

    public bool IsRewardsClaimed { get; set; }


    public string RankName { get => rankName; }
    public Sprite Icon { get => icon; }
    public int RPRequire { get => rPRequire; }
    public ItemClaim[] InstantReward { get => instantReward; }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            IsRewardsClaimed = false;
            return;
        }

        IsRewardsClaimed = saveData.IsRewardClaimed;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.IsRewardClaimed = IsRewardsClaimed;
        return JsonUtility.ToJson(saveData);
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private bool ic;

        public bool IsRewardClaimed { get => ic; set => ic = value; }
    }
}
