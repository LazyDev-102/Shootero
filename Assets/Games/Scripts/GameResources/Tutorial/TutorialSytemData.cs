using SimpleJSON;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialSytemData", menuName = "Tutorial/TutorialSytemData")]
public class TutorialSytemData : ScriptableObject {
    [SerializeField] private bool finishTutorialIntroduce;
    [SerializeField] private bool finishTutorialOpenChest;
    [SerializeField] private bool finishTutorialEquipment;
    [SerializeField] private bool finishTutorialPlayGame;
    [SerializeField] private bool finishTutorialOpenSkill;
    [SerializeField] private bool finishTutorialEquipSkills;
    [SerializeField] private bool gaveSkill;
    [SerializeField] private bool gaveKey;
    [SerializeField] private ItemStack rewardKey;
    [SerializeField] private bool gaveEnergy;
    [SerializeField] private ItemStack rewardEnergy;
    [SerializeField] private bool finishInfinityTutorial;

    private bool firstConditonInfinity; // Play Done Zone 5

    public bool FinishFree => finishTutorialIntroduce && finishTutorialOpenChest && finishTutorialEquipment;
    public bool FinishAllTutorial { get => finishTutorialIntroduce && finishTutorialOpenChest && finishTutorialEquipment && finishTutorialPlayGame; }
    public bool FinishTutorialIntroduce { get => finishTutorialIntroduce; }
    public bool FinishTutorialOpenChest { get => finishTutorialOpenChest; }
    public bool FinishTutorialEquipment { get => finishTutorialEquipment; }
    public bool FinishTutorialOpenSkill { get => finishTutorialOpenSkill; }
    public bool FinishTutorialEquipSkills { get => finishTutorialEquipSkills; }
    public bool FinishTutorialPlayGame { get => finishTutorialPlayGame; }
    public bool FinishInfinityTutorial { get => finishInfinityTutorial; }
    public bool GaveKey { get => gaveKey; }
    public bool GaveEnergy { get => gaveEnergy; }
    public bool GaveSkill { get => gaveSkill; }

    [HideInInspector]
    public bool IsOpenSkillTutorial;

    public void SetFinishAllTutorial() {
        SetFinishTutorialEquipment(true)
        .SetFinishTutorialIntroduce(true)
        .SetFinishTutorialOpenChest(true)
        .SetFinishTutorialPlayGame(true)
        .SetGaveEnergy(true)
        .SetGaveKey(true);
    }

    public TutorialSytemData SetFinishTutorialIntroduce(bool value) {
        finishTutorialIntroduce = value;
        return this;
    }
    public TutorialSytemData SetFinishTutorialOpenChest(bool value) {
        finishTutorialOpenChest = value;
        return this;
    }
    public TutorialSytemData SetFinishTutorialEquipment(bool value) {
        finishTutorialEquipment = value;
        return this;
    }
    public TutorialSytemData SetFinishTutorialPlayGame(bool value) {
        finishTutorialPlayGame = value;
        return this;
    }
    public TutorialSytemData SetFinishTutorialPlayInfinity(bool value) {
        finishInfinityTutorial = value;
        return this;
    }
    public TutorialSytemData SetFinishTutorialEquipSkills(bool value) {
        finishTutorialEquipSkills = value;
        IsOpenSkillTutorial = false;
        return this;
    }
    public TutorialSytemData SetFinishTutorialOpenSkill(bool value) {
        finishTutorialOpenSkill = value;
        return this;
    }
    public TutorialSytemData SetGaveKey(bool value) {
        gaveKey = value;
        return this;
    }
    public TutorialSytemData SetGaveEnergy(bool value) {
        gaveEnergy = value;
        return this;
    }
    public TutorialSytemData SetGaveSkill(bool value) {
        gaveSkill = value;
        return this;
    }
    public TutorialSytemData GetRewardKey() {
        if (gaveKey)
            return this;
        SetGaveKey(true);
        if (GameResources.Instance.Inventory.GetItem(rewardKey.Id).Amount == 0)
            GameResources.Instance.Inventory.Add(rewardKey.Id, 1);
        return this;
    }
    public void GetRewardEnergy() {
        if (gaveEnergy)
            return;
        SetGaveEnergy(true);
        GameResources.Instance.Inventory.Add(rewardEnergy.Id, rewardEnergy.Amount);
    }
    public void SetFirstConditionPlayInfinity(bool status) {
        firstConditonInfinity = status;
    }
    public bool CanShowTutorialPlayInfinity() {
        return FinishFree && firstConditonInfinity && !finishInfinityTutorial && GameResources.Instance.ConquerorData.UnlockZone > 4;
    }
    public bool CanShowEquipSkillsTutorial() {
        return FinishFree && !finishTutorialEquipSkills && finishTutorialOpenSkill;
    }
    public bool CanShowOpenSkillTutorial() {
        return FinishFree && !finishTutorialOpenSkill && CanActiveSkills();
    }
    public bool CanActiveSkills() {
        return GameResources.Instance.ConquerorData.UnlockZone > 2;
    }
    #region Save Load
    private void OnEnable() {
        InitializeData();
    }
    private void InitializeData() {
        finishTutorialIntroduce = false;
        finishTutorialOpenChest = false;
        finishTutorialEquipment = false;
        finishTutorialPlayGame = false;
        finishTutorialEquipSkills = false;
        finishTutorialOpenSkill = false;
        finishInfinityTutorial = false;
        gaveKey = false;
        gaveEnergy = false;
        gaveSkill = false;
        IsOpenSkillTutorial = false;
    }
    private void ActionOnLoad() {
        gaveSkill = finishTutorialOpenSkill;
        if (gaveSkill)
            IsOpenSkillTutorial = false;
    }
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            InitializeData();
            return;
        }
        finishTutorialIntroduce = saveData.FinishTutorialInGame;
        finishTutorialOpenChest = saveData.FinishTutorialOpenChest;
        finishTutorialEquipment = saveData.FinishTutorialEquipment;
        finishTutorialEquipSkills = saveData.FinishTutorialEquipSkills;
        finishTutorialOpenSkill = saveData.FinishTutorialOpenSkill;
        finishTutorialPlayGame = saveData.FinishTutorialPlayGame;
        finishInfinityTutorial = saveData.FinishTutorialPlayInfinity;
        gaveKey = saveData.GaveKey;
        gaveEnergy = saveData.GaveEnergy;
        gaveSkill = saveData.GaveSkill;
        if (finishTutorialOpenSkill)
            IsOpenSkillTutorial = false;
        ActionOnLoad();
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.FinishTutorialInGame = finishTutorialIntroduce;
        saveData.FinishTutorialOpenChest = finishTutorialOpenChest;
        saveData.FinishTutorialEquipment = finishTutorialEquipment;
        saveData.FinishTutorialOpenSkill = finishTutorialOpenSkill;
        saveData.FinishTutorialEquipSkills = finishTutorialEquipSkills;
        saveData.FinishTutorialPlayGame = finishTutorialPlayGame;
        saveData.FinishTutorialPlayInfinity = finishInfinityTutorial;
        saveData.GaveKey = gaveKey;
        saveData.GaveEnergy = gaveEnergy;
        saveData.GaveSkill = gaveSkill;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            InitializeData();
        }
        else {
            finishTutorialIntroduce = json[JsonKey.FinishTutorialInGame].AsBool;
            finishTutorialOpenChest = json[JsonKey.FinishTutorialOpenChest].AsBool;
            finishTutorialEquipment = json[JsonKey.FinishTutorialEquipment].AsBool;
            finishTutorialEquipSkills = json[JsonKey.FinishTutorialEquipSkills].AsBool;
            finishTutorialOpenSkill = json[JsonKey.FinishTutorialOpenSkill].AsBool;
            finishTutorialPlayGame = json[JsonKey.FinishTutorialPlayGame].AsBool;
            finishInfinityTutorial = json[JsonKey.FinishTutorialPlayInfinity].AsBool;
            gaveKey = json[JsonKey.GaveKey].AsBool;
            gaveEnergy = json[JsonKey.GaveEnergy].AsBool;
            gaveSkill = json[JsonKey.GaveSkill].AsBool;
            ActionOnLoad();
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.FinishTutorialInGame, finishTutorialIntroduce);
        node.Add(JsonKey.FinishTutorialOpenChest, finishTutorialOpenChest);
        node.Add(JsonKey.FinishTutorialEquipment, finishTutorialEquipment);
        node.Add(JsonKey.FinishTutorialOpenSkill, finishTutorialOpenSkill);
        node.Add(JsonKey.FinishTutorialEquipSkills, finishTutorialEquipSkills);
        node.Add(JsonKey.FinishTutorialPlayGame, finishTutorialPlayGame);
        node.Add(JsonKey.FinishTutorialPlayInfinity, finishInfinityTutorial);
        node.Add(JsonKey.GaveKey, gaveKey);
        node.Add(JsonKey.GaveEnergy, gaveEnergy);
        node.Add(JsonKey.GaveSkill, gaveSkill);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private bool fti;
        [SerializeField] private bool fte;
        [SerializeField] private bool ftos;
        [SerializeField] private bool ftes;
        [SerializeField] private bool foc;
        [SerializeField] private bool fpg;
        [SerializeField] private bool gk;
        [SerializeField] private bool ge;
        [SerializeField] private bool gs;
        [SerializeField] private bool fpi;

        public bool FinishTutorialInGame { get => fti; set => fti = value; }
        public bool FinishTutorialOpenChest { get => foc; set => foc = value; }
        public bool FinishTutorialEquipment { get => fte; set => fte = value; }
        public bool FinishTutorialEquipSkills { get => ftes; set => ftes = value; }
        public bool FinishTutorialOpenSkill { get => ftos; set => ftos = value; }
        public bool FinishTutorialPlayGame { get => fpg; set => fpg = value; }
        public bool FinishTutorialPlayInfinity { get => fpi; set => fpi = value; }
        public bool GaveKey { get => gk; set => gk = value; }
        public bool GaveEnergy { get => ge; set => ge = value; }
        public bool GaveSkill { get => gs; set => gs = value; }
    }
    #endregion
}
