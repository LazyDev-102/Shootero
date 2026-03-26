using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "LifeLeechModData", menuName = "Mod/KillModData/LifeLeech")]
public class LifeLeechModData : KillModData {
    [SerializeField] private float hpPercent;

    public float HpPercent { get => hpPercent; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        LifeLeechModInfo modInfo = new LifeLeechModInfo(this);
        character.ShipSkill.AddKillAttackMod(modInfo);
    }
}

public class LifeLeechModInfo : KillModInfor<LifeLeechModData> {

    public LifeLeechModInfo(LifeLeechModData modData) : base(modData) {
    }

    public LifeLeechModInfo(LifeLeechModInfo mod) : base(mod) {

    }

    public override void ActionKill(ShipBase killer, CharacterBase victim) {
        int maxHp = killer.CharacterStat.MaxHP.Value;
        int hpLeech = Mathf.CeilToInt(maxHp * modData.HpPercent);
        killer.ShipHealth.AddHpWithHealingEffect(hpLeech, true);
    }

    public override object Clone() {
        return new LifeLeechModInfo(this);
    }
}



