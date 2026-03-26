
using Gemmob;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSkillData : Item {
    [SerializeField] protected SkillType type;
    [SerializeField] protected int currentRank;
    [SerializeField] protected int levelUnlock = 0;
    [SerializeField] private int amount;
    [SerializeField] protected SkillRankData[] skillRankData;
    [SerializeField] protected SkillRankData[] privateSkillRankData;

    public bool IsOwn => amount > 0 || currentRank > 0;
    public bool IsPassive => type == SkillType.Passive;
    public string TagName => IsPassive ? "(Passive)" : "(Active)";
    public int Rank { get => currentRank; }
    public int Amount { get => amount; }
    public bool IsNew;

    public bool IsMaxRank => currentRank >= skillRankData.Length - 1;
    protected bool canAttack;
    protected ShipBase ship;

    private void OnEnable() {
        InitData();
    }

    public void InitData() {
        currentRank = 0;
        amount = 0;
        IsNew = false;
    }
    public void InitData(int rank, int a, bool isNew) {
        currentRank = rank;
        amount = a;
        IsNew = isNew;
    }
    public bool IsEquip() {
        return Id == GameResources.Instance.SkillSystemData.GetSkillSelectedId();
    }
    public virtual bool CanApplyTo() {
        return GameResources.Instance.LevelProgress.GetCurrentLevel() >= levelUnlock;
    }
    public virtual void ApplyTo() {
        GameResources.Instance.SkillSystemData.AddSkill(this);
        Preload();
    }
    public virtual bool IsReady(ShipBase ship) {
        return true;
    }
    public virtual void StartAttack(ShipBase ship) {
        //Claim(-1);
        this.ship = ship;
    }
    public virtual void Updating() {

    }
    public virtual void EndAttack(ShipBase ship) {
        this.ship = ship;
    }
    public virtual void ResetData() {

    }
    public virtual void Preload() {

    }
    public virtual IEnumerable<SkillRankItemData> GetDescription(int rank) {
        foreach (var value in skillRankData[rank].Datas) {
            yield return value;
        }
    }
    public float GetStat(SkillRankItemType type) {
        int index = currentRank;
        if (index >= skillRankData.Length)
            index = skillRankData.Length - 1;
        return skillRankData[index].GetStat(type);
    }
    public float GetStat(SkillRankItemType type, int index) {
        if (index >= skillRankData.Length)
            index = skillRankData.Length - 1;
        return skillRankData[index].GetStat(type);
    }
    public float GetNextStat(SkillRankItemType type) {
        int index = currentRank + 1;
        if (index >= skillRankData.Length)
            index = skillRankData.Length - 1;
        return skillRankData[index].GetStat(type);
    }
    public float GetPrivateStat(SkillRankItemType type) {
        int index = currentRank;
        if (index >= privateSkillRankData.Length)
            index = privateSkillRankData.Length - 1;
        return privateSkillRankData[index].GetStat(type);
    }
    public virtual string GetDescription(bool hasNext) {
        if (!hasNext)
            return GetCurrentDescription();
        if (IsMaxRank)
            return GetCurrentDescription();
        else
            return GetNextDescription();
    }
    public virtual string GetDescriptionByIndex(int index) {
        return Description;
    }
    protected virtual string GetCurrentDescription() {
        return Description;
    }
    protected virtual string GetNextDescription() {
        return Description;
    }
    public string GetAmountDescription() {
        return IsMaxRank? "Max" : $"{amount}/{PieceNeedToUpgrade()}";
    }
    public bool CanUpgradable() {
        return IsMaxRank ? false : amount >= PieceNeedToUpgrade();
    }
    public float GetRatio() {
        float ratio = (float)amount / (float)PieceNeedToUpgrade();
        if (ratio > 1)
            ratio = 1;
        return ratio;
    }
    private int PieceNeedToUpgrade() {
        return GameResources.Instance.SkillSystemData.GetPieceNeedToUpgrade(currentRank);
    }
    public void Upgrade() {
        UpdateAmount();
        currentRank++;
    }
    private void UpdateAmount() {
        amount -= PieceNeedToUpgrade();
        if (amount < 0)
            amount = 0;
    }
    public override void Claim(int amount) {
        if (this.amount == 0 && currentRank == 0)
            IsNew = true;
        this.amount += amount;
    }
}

[System.Serializable]
public class SkillRankData {
    [SerializeField] private SkillRankItemData[] datas;
    public SkillRankItemData[] Datas => datas;

    public float GetStat(SkillRankItemType type) {
        var skill = Datas.FirstOrDefault(x => x.Type == type);
        if (skill == null) {
            Logs.LogError("Null Skill Stat");
            return 0;
        }
        return skill.Value.Value;
    }
}

[System.Serializable]
public class SkillRankItemData {
    public string Name;
    public SkillRankItemType Type;
    public StatModifier Value;
}
public enum SkillRankItemType {
    CoolDown,
    FireRate,
    DeltaShot,
    Duration,
    FlatDamage,
    PercentDamage,
    FlatHp,
    PercentHp,
    BulletCount,
    BulletSpeed,
    BulletAimSpeed,
    TimeHoming,
    DelayHoming,
}
