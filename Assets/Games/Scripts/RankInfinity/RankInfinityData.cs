using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RankInfinityData", menuName = "Resource/Modes/Infinity/RankInfinityData")]
public class RankInfinityData : ScriptableObject, ISaveLoadable {
    private int rankPoint;
    private int highScore;
    public int RankPoint {
        get => rankPoint;
        set {
            if (rankPoint != value) {
                rankPoint = value;
            }
        }
    }
    public int HighScore {
        get => highScore;
        set {
            if (highScore > value) {
                highScore = value;
            }
        }
    }


    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.RankPoint = rankPoint;
        saveData.HighScore = highScore;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            rankPoint = 0;
            highScore = 0;
            return;
        }
        rankPoint = saveData.RankPoint;
        highScore = saveData.HighScore;
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.RankPoint, rankPoint);
        node.Add(JsonKey.HighScore, highScore);
        return node;
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            rankPoint = 0;
            highScore = 0;
        }
        else {
            rankPoint = json[JsonKey.RankPoint].AsInt;
            highScore = json[JsonKey.HighScore].AsInt;
        }
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private int rp;
        [SerializeField] private int hs;

        public int RankPoint { get => rp; set => rp = value; }
        public int HighScore { get => hs; set => hs = value; }

    }
}