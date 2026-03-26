using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "BlastShotModData", menuName = "Mod/EffectAttack/BlastShot")]

public class BlastShotModData : EffectAttackModData {
    [SerializeField] private float chance;
    [SerializeField] private float radius;
    [SerializeField] private float damagePercent;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private int numberPreload;

    public float Radius { get => radius; }
    public float DamagePercent { get => damagePercent; }
    public Explosioner Explosioner { get => explosioner; }
    public float Chance { get => chance; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipSkill.AddEffectAttackMod(new BlastShotModInfor(this));
    }

    public override void PreloadOpenApp() {
        if (explosioner) {
            explosioner.PreloadIngame();
            explosioner.RegisterPool(numberPreload);
        }
    }
}


public class BlastShotModInfor : EffectAttackModInfor<BlastShotModData> {
    private FloatStat radius;
    private FloatStat damagePercent;

    public FloatStat Radius { get => radius; }
    public FloatStat DamagePercent { get => damagePercent; }

    public BlastShotModInfor(BlastShotModData mod) : base(mod) {
        radius = new FloatStat(mod.Radius);
        damagePercent = new FloatStat(mod.DamagePercent);
    }

    public BlastShotModInfor(BlastShotModInfor mod) : base(mod) {
        radius = new FloatStat(mod.radius);
        damagePercent = new FloatStat(mod.damagePercent);
    }

    public override void EffectTo(CharacterBase victim, ObjectBase causer, IntStat damageStat, Vector2 position) {
        if (RandomHelper.RandomWithPercent(modData.Chance)) {
            int damage = Mathf.RoundToInt(damageStat.Value * damagePercent.Value);
            float radiusValue = radius.Value;
            if (causer is ShipBase ship) {
                radiusValue *= (1 + ship.ShipStat.BlastRadiusPercent.Value);
            }
            else if (causer is DroneBase drone) {
                radiusValue *= (1 + drone.DroneStat.BlastRadiusPercent.Value);
            }
            Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(modData.Explosioner, position);
            if (newExplosioner) {
                newExplosioner.SetHitInfor(damage, null, causer)
                            .SetRadius(radiusValue)
                            .Explosioning();
            }
        }
    }

    public override object Clone() {
        return new BlastShotModInfor(this);
    }

    public void ChangeRadius(StatModifier modifier) {
        this.radius.AddModifier(modifier);
    }
}