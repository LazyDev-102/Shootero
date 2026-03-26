using Gemmob;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathExplosionModData", menuName = "Mod/KillModData/DeathExplosion")]
public class DeathExplosionModData : KillModData {
    [SerializeField] private int chance;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private float radius;
    [SerializeField] private float damagePercent;
    [SerializeField] private int numberPreload;

    public int Chance { get => chance; }
    public float Radius { get => radius; }
    public float DamagePercent { get => damagePercent; }
    public Explosioner Explosioner { get => explosioner; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        DeathExplosionModInfo modInfo = new DeathExplosionModInfo(this);
        character.ShipSkill.AddKillAttackMod(modInfo);
    }

    public override void PreloadOpenApp() {
        if (explosioner) {
            explosioner.PreloadIngame();
            explosioner.RegisterPool(numberPreload);
        }
    }
}

public class DeathExplosionModInfo : KillModInfor<DeathExplosionModData> {

    public DeathExplosionModInfo(DeathExplosionModData modData) : base(modData) {
    }

    public DeathExplosionModInfo(DeathExplosionModInfo mod) : base(mod) {

    }

    public override void ActionKill(ShipBase killer, CharacterBase victim) {
        if (RandomHelper.RandomWithProbability(modData.Chance)) {
            Vector2 position = victim.transform.position;
            killer.DelayFrame(1, () => {
                Explosing(killer, position);
            });
        }
    }

    private void Explosing(ShipBase ship, Vector2 position) {
        int damage = Mathf.RoundToInt(ship.ShipStat.GetFinalDamageWeapon * modData.DamagePercent);
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(modData.Explosioner, position);
        if (newExplosioner) {
            newExplosioner.SetHitInfor(damage, null, ship)
                        .SetRadius(modData.Radius)
                        .Explosioning();
        }
    }


    public override object Clone() {
        return new DeathExplosionModInfo(this);
    }
}



