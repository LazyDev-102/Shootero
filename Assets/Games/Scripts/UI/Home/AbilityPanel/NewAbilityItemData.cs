using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityItemData", menuName = "Resource/HardData/Ability/NewAbilityItemData")]
public class NewAbilityItemData : ScriptableObject {
    [SerializeField] private Sprite icon;
    [SerializeField] private string abilityName;
    [SerializeField] private string abilityDescription;
    [SerializeField] private int level;
    [SerializeField] private int pointRequire = 1;
    [SerializeField] private AbilityStatInfo abilityStat;
    [SerializeField] private AbilityRequireInfo[] requires;
    [SerializeField] private bool isSpecial;

    public int Level { get => level; set => level = value; }
    public int PointRequire { get => pointRequire; }
    public Sprite Icon { get => icon; }
    public string Name { get => abilityName; }
    public string Description { get => abilityDescription; }
    public bool IsMaxLevel => level == abilityStat.Datas.Length;
    public bool IsSpecial { get => isSpecial; }
    public bool Unlocked => level > 0;

    protected virtual void OnEnable() {
        level = 0;
    }

    public bool CanUnlock() {
        if (requires == null || requires.Length == 0)
            return true;
        foreach (var item in requires) {
            if (!item.EnoughCondition())
                return false;
        }
        return true;
    }

    public virtual void Apply(ShipBase ship) {
        if (Unlocked)
            Install();
    }
    public virtual void ApplyIngame(ShipBase ship) {
        if (Unlocked)
            Install();
    }

    public void ResetAll() {
        Unistall();
        level = 0;
    }

    public void LevelUp() {
        Unistall();
        level++;
        Install();
    }

    protected virtual void Install() {
        if (abilityStat != null)
            abilityStat.AddStat(level - 1);
    }

    protected virtual void Unistall() {
        if (abilityStat != null)
            abilityStat.RemoveStat(level - 1);
    }

    private bool NotNull() {
        return abilityStat != null && abilityStat.StatData != null;
    }
    public string GetCurrentvalue() {
        return NotNull() ? abilityStat.GetValueString(level - 1) : "";
    }
    public string GetNextvalue() {
        return NotNull() ? abilityStat.GetValueString(IsMaxLevel ? level - 1 : level) : "";
    }
    public string GetUnlockValue() {
        return NotNull() ? abilityStat.GetValueString(0) : "";
    }
}
