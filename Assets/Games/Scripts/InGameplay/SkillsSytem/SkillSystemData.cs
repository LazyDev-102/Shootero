
using SimpleJSON;
using UnityEngine;
using System;
using Helper;

[CreateAssetMenu(fileName = "SkillSystemData", menuName = "Resource/HardData/Skill/SkillSystemData")]
public class SkillSystemData : ScriptableObject {
    [SerializeField] private int getx10In;
    [SerializeField] private ItemCollector allSkills;
    [SerializeField, Space] private SkillsUpgradeInfor[] pieceNeedToUpgrade;
    [SerializeField, Space] private SkillsPackData pack;

    public int Getx10In { get => getx10In; }
    private ItemSkillData skillSelected;
    [Space, Space]
    public ItemSkillData[] AllSkills;
    public SkillsPackData Pack { get => pack; }
    public bool IsPassive => skillSelected != null && skillSelected.IsPassive;
    public bool HasSkill => skillSelected != null;

    public void Reload() {
        AllSkills = new ItemSkillData[allSkills.Items.Length];
        for (int i = 0; i < allSkills.Items.Length; i++) {
            ItemSkillData newItem = (ItemSkillData)allSkills.Items[i];
            AllSkills[i] = newItem;
        }
        skillSelected = null;
    }

    public void AddSkill(ItemSkillData skill) {
        skillSelected = skill;
    }
    public void RemoveSkill() {
        skillSelected = null;
    }
    public void RemoveAllSkill() {
        skillSelected = null;
    }
    public void ApplyTo() {
        if (skillSelected != null) {
            skillSelected.ApplyTo();
        }
    }
    public bool IsReady(ShipBase ship) {
        if (skillSelected == null)
            return false;
        return skillSelected.IsReady(ship);
    }
    public ItemSkillData GetSkillSelected() {
        if (skillSelected != null && !skillSelected.IsOwn)
            skillSelected = null;
        return skillSelected;
    }
    public int GetSkillSelectedId() {
        return skillSelected == null ? -1 : skillSelected.Id;
    }
    public Sprite GetSkillSelectIcon() {
        return skillSelected != null ? skillSelected.Icon : null;
    }
    public string GetSkillSelectName() {
        return skillSelected != null ? skillSelected.Name : "";
    }
    public string GetSkillSelectDescription() {
        return skillSelected != null ? skillSelected.Description : "";
    }
    public float GetTimeCooldown() {
        return skillSelected != null ? skillSelected.GetStat(SkillRankItemType.CoolDown) : 100;
    }
    public bool UpgradeableAnySkill() {
        foreach (var item in AllSkills) {
            if (item.IsNew || item.CanUpgradable())
                return true;
        }
        return false;
    }

    public void StartAttack(ShipBase ship) {
        if (skillSelected != null)
            skillSelected.StartAttack(ship);
    }
    public void EndAttack(ShipBase ship) {
        if (skillSelected != null)
            skillSelected.EndAttack(ship);
    }
    public void Updating() {
        if (skillSelected != null)
            skillSelected.Updating();
    }

    public int GetPieceNeedToUpgrade(int rank) {
        if (pieceNeedToUpgrade.Length <= rank)
            return 100;
        return pieceNeedToUpgrade[rank].Piece;
    }
    public void UpdateGetX10In() {
        getx10In--;
        if (getx10In <= 0) {
            getx10In = 10;
        }
    }
    public int GetRewardCount() {
        return pack.GetRewardCount(getx10In == 1);
    }
    public ItemSkillData GetRandomSkill() {
        return RandomHelper.RandomInCollection(AllSkills);
    }
    public void ClaimReward(ItemSkillData item, int count) {
        UpdateGetX10In();
        foreach (var skill in AllSkills) {
            if (skill.Id == item.Id) {
                skill.Claim(count);
                break;
            }
        }
    }
    private void OnEnable() {
        Reload();
    }

    #region Save Load Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData != null) {
            getx10In = saveData.GetX10In;
            int length = saveData.Ranks.Length;
            if (saveData.IsNew == null || saveData.IsNew.Length == 0)
                saveData.IsNew = new bool[length];
            if (saveData.Ranks == null || saveData.Ranks.Length == 0)
                saveData.Ranks = new int[length];
            for (int i = 0; i < length; i++) {
                AllSkills[i].InitData(saveData.Ranks[i], saveData.Amounts[i], saveData.IsNew[i]);
                if (AllSkills[i].Id == saveData.SkillId)
                    skillSelected = AllSkills[i];
            }
        }
        else {
            skillSelected = null;
            getx10In = 10;
            for (int i = 0; i < AllSkills.Length; i++) {
                AllSkills[i].InitData();
            }
        }
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        int length = AllSkills.Length;
        saveData.SkillId = skillSelected != null ? skillSelected.Id : -1;
        saveData.GetX10In = getx10In;
        saveData.Ranks = new int[length];
        saveData.Amounts = new int[length];
        saveData.IsNew = new bool[length];
        for (int i = 0; i < length; i++) {
            saveData.Ranks[i] = AllSkills[i].Rank;
            saveData.Amounts[i] = AllSkills[i].Amount;
            saveData.IsNew[i] = AllSkills[i].IsNew;
        }
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            skillSelected = null;
            getx10In = 10;
            for (int i = 0; i < AllSkills.Length; i++) {
                AllSkills[i].InitData();
            }
        }
        else {
            var cSkillId = json[JsonKey.ItemId].AsInt;
            getx10In = json[JsonKey.CurrentRemain].AsInt;

            JSONArray rank = json[JsonKey.Rank].AsArray;
            JSONArray amount = json[JsonKey.Amount].AsArray;
            JSONArray isNew = json[JsonKey.IsNew].AsArray;

            for (int i = 0; i < rank.Count; i++) {
                AllSkills[i].InitData(rank[i].AsInt, amount[i].AsInt, isNew[i].AsBool);
                if (AllSkills[i].Id == cSkillId)
                    skillSelected = AllSkills[i];
            }
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.ItemId, skillSelected != null ? skillSelected.Id : -1);
        node.Add(JsonKey.CurrentRemain, getx10In);

        int length = AllSkills.Length;

        JSONNode rankNote = new JSONArray();
        JSONNode amountNote = new JSONArray();
        JSONNode isNewNote = new JSONArray();

        for (int i = 0; i < length; i++) {
            rankNote.Add(AllSkills[i].Rank);
            amountNote.Add(AllSkills[i].Amount);
            isNewNote.Add(AllSkills[i].IsNew);
        }
        node.Add(JsonKey.Rank, rankNote);
        node.Add(JsonKey.Amount, amountNote);
        node.Add(JsonKey.IsNew, isNewNote);

        return node;
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private int ssi;
        [SerializeField] private int x10;
        [SerializeField] private int[] r;
        [SerializeField] private int[] a;
        [SerializeField] private bool[] isNew;

        public int SkillId { get => ssi; set => ssi = value; }
        public int GetX10In { get => x10; set => x10 = value; }
        public int[] Ranks { get => r; set => r = value; }
        public int[] Amounts { get => a; set => a = value; }
        public bool[] IsNew { get => isNew; set => isNew = value; }
    }
    #endregion

    [Serializable]
    public class SkillsUpgradeInfor {
        public int Rank;
        public int Piece;
    }
}
[Serializable]
public class SkillsPackData {
    [SerializeField] private ItemStack price;
    [SerializeField] private RewardRate[] rewardRate;

    public ItemStack Price { get => price; }

    public int GetRewardCount(bool forceX10) {
        if (forceX10)
            return rewardRate[rewardRate.Length - 1].RewardCount;

        var ran = UnityEngine.Random.Range(0, 101);
        for (int i = 0; i < rewardRate.Length; i++) {
            if (ran <= rewardRate[i].Rate) {
                return rewardRate[i].RewardCount;
            }
        }
        return rewardRate[0].RewardCount;
    }

    [Serializable]
    public struct RewardRate {
        public int RewardCount;
        public float Rate;
    }
}