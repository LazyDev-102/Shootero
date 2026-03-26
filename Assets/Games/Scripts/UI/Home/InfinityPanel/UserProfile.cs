using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Resource/HardData/User/UserProfile")]
public class UserProfile : ScriptableObject {
    [SerializeField] private bool usingCloud;
    [SerializeField] private UserProfileInfo myInfo;
    [SerializeField] private List<UserProfileInfo> userProfiles;

    public List<UserProfileInfo> Data { get => userProfiles; set => userProfiles = value; }
    public bool UsingCloud { get => usingCloud; set => usingCloud = value; }
    public UserProfileInfo MyInfo { get => myInfo; set => myInfo = value; }

    public void SetUsingCloud(bool status) {
        usingCloud = status;
    }
    public string GetIngameName() {
        if (myInfo == null)
            return "";
        return myInfo.PlayerName;
    }
    public int GetHighScore() {
        if (myInfo == null)
            return 0;
        return myInfo.PlayerScore;
    }
    public int GetLevel() {
        if (myInfo == null)
            return 0;
        return myInfo.PlayerLevel;
    }
    public string GetInfo() {
        return "Nothing";
    }
    public void SetRank(int rank) {
        if (myInfo == null)
            return;
        myInfo.PlayerRank = rank;
    }
    public void SetPoint(int point) {
        if (myInfo == null)
            myInfo = new UserProfileInfo();
        if (point > myInfo.PlayerScore)
            myInfo.PlayerScore = point;
    }
    public void SetMyInfo(UserProfileInfo info) {
        if (myInfo == null)
            myInfo = new UserProfileInfo();
        myInfo.PlayerName = info.PlayerName != null ? info.PlayerName : myInfo.PlayerName;
        myInfo.PlayerLevel = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
        myInfo.PlayerLevel = myInfo.PlayerLevel < 0 ? 0 : myInfo.PlayerLevel >= 99 ? 99 : myInfo.PlayerLevel;
    }

    public void LoadData(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData == null) {
            myInfo.PlayerName = "";
            myInfo.PlayerScore = 0;
            return;
        }
        myInfo = saveData.UserProfileInfo;
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.UserProfileInfo = myInfo;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            myInfo.PlayerName = "";
            myInfo.PlayerScore = 0;
        }
        else {
            myInfo.LoadFJson(json[JsonKey.UserProfileInfo]);
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.UserProfileInfo, myInfo.Save2Json());
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private string n;
        [SerializeField] private UserProfileInfo userProfileInfo;

        public string UserId { get => n; set => n = value; }
        public UserProfileInfo UserProfileInfo { get => userProfileInfo; set => userProfileInfo = value; }
    }
}
[System.Serializable]
public class UserProfileInfo {
    [SerializeField] private int r;
    [SerializeField] private string n;
    [SerializeField] private int l;
    [SerializeField] private int s;

    public int PlayerRank { get => r; set => r = value; }
    public string PlayerName { get => n; set => n = value; }
    public int PlayerLevel { get => l; set => l = value; }
    public int PlayerScore { get => s; set => s = value; }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            r = 9999;
            n = @"Your Name";
            l = 1;
            s = 0;
        }
        else {
            r = json[JsonKey.Rank].AsInt;
            n = json.HasKey(JsonKey.YourName) ? json[JsonKey.YourName].Value : "";
            l = json[JsonKey.CurrentLv].AsInt;
            s = json[JsonKey.HighScore].AsInt;
            l = l < 0 ? 0 : l >= 99 ? 99 : l;
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Rank, r);
        node.Add(JsonKey.YourName, n);
        node.Add(JsonKey.CurrentLv, l);
        node.Add(JsonKey.HighScore, s);
        return node;
    }
}