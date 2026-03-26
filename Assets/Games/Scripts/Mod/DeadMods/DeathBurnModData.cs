using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathBurnModData", menuName = "Mod/KillModData/DeathBurn")]
public class DeathBurnModData : KillModData {
    [SerializeField] private int chance;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    [SerializeField] private BurnShotModData burnShotMod;

    public int Chance { get => chance; }
    public float Radius { get => radius; }
    public LayerMask Layer { get => layer; }
    public BurnShotModData BurnShotMod { get => burnShotMod; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        DeathBurnModInfo modInfo = new DeathBurnModInfo(this);
        character.ShipSkill.AddKillAttackMod(modInfo);
    }
}

public class DeathBurnModInfo : KillModInfor<DeathBurnModData> {

    public DeathBurnModInfo(DeathBurnModData modData) : base(modData) {
    }

    public DeathBurnModInfo(DeathBurnModInfo mod) : base(mod) {

    }

    public override void ActionKill(ShipBase killer, CharacterBase victim) {
        if (RandomHelper.RandomWithProbability(modData.Chance)) {
            Vector2 position = victim.transform.position;
            killer.DelayFrame(1, () => {
                Burn(killer, position);
            });
        }
    }

    private void Burn(ShipBase ship, Vector2 position) {
        Collider2D[] targets = Physics2D.OverlapCircleAll(position, modData.Radius, modData.Layer);
        BurnShotModInfor burnShotModInfo = ship.ShipSkill.GetModInfor<BurnShotModInfor>(modData.BurnShotMod.ModId);
        if (burnShotModInfo == null) {
            return;
        }
        foreach (var target in targets) {
            CharacterBase character = target.GetComponent<CharacterBase>();
            if (character) {
                int damage = Mathf.RoundToInt(ship.ShipStat.GetFinalDamageWeapon * burnShotModInfo.DamagePercent.Value);
                BurnEffect effect = new BurnEffect(character, ship, burnShotModInfo.Duration.Value, burnShotModInfo.DeltaBurn.Value, damage, burnShotModInfo.MaxBurnStack);
                character.CharacterSkill.AddCountdownEffect(effect);
            }
        }
    }


    public override object Clone() {
        return new DeathBurnModInfo(this);
    }
}


