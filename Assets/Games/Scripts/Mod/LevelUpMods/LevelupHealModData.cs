using UnityEngine;


[CreateAssetMenu(fileName = "LevelupHealModData", menuName = "Mod/LevelupModData/LevelupHeal")]
public class LevelupHealModData : LevelupModData {
    [SerializeField] private float healPercent;

    public float HealPercent { get => healPercent; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        LevelupHealInfo modInfo = new LevelupHealInfo(this);
        character.ShipSkill.AddLevelupMod(modInfo);
    }
}

public class LevelupHealInfo : LevelupModInfo<LevelupHealModData> {
    public LevelupHealInfo(LevelupHealModData modData) : base(modData) {

    }

    public LevelupHealInfo(LevelupHealInfo modInfo) : base(modInfo) {

    }

    public override void ActionLevelup(ShipBase ship) {
        int maxHp = ship.ShipStat.MaxHP.Value;
        int healHp = Mathf.CeilToInt(maxHp * modData.HealPercent);
        ship.ShipHealth.AddHpWithHealingEffect(healHp, true);
    }

    public override object Clone() {
        return new LevelupHealInfo(this);
    }
}
