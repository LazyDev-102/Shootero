using SimpleJSON;
using UnityEngine;


[CreateAssetMenu(fileName = "IapPackData", menuName = "Resource/HardData/IapPack/IapPackData")]
public class IapPackData : ScriptableObject {
    [SerializeField] private ResourcePackData resourcePack;
    [SerializeField] private SmartOfferData smartOffer;

    public ResourcePackData ResourcePack { get => resourcePack; }
    public SmartOfferData SmartOffer { get => smartOffer; }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            resourcePack.LoadFromJson("");
            smartOffer.LoadFromJson("");
            return;
        }
        resourcePack.LoadFromJson(saveData.ResourcePack);
        smartOffer.LoadFromJson(saveData.SmartOffer);
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.ResourcePack = resourcePack.SaveToJson();
        saveData.SmartOffer = smartOffer.SaveToJson();
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            resourcePack.LoadFJson(null);
            smartOffer.LoadFJson(null);
        }
        else {
            resourcePack.LoadFJson(json[JsonKey.ResourcePack]);
            smartOffer.LoadFJson(json[JsonKey.SmartOffer]);
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.ResourcePack, resourcePack.Save2Json());
        node.Add(JsonKey.SmartOffer, smartOffer.Save2Json());
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private string rp;
        [SerializeField] private string so;

        public string ResourcePack { get => rp; set => rp = value; }
        public string SmartOffer { get => so; set => so = value; }
    }
}
