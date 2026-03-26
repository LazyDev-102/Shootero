using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "FireballSatelliteModData", menuName = "Mod/Buff/Limited/FireballSatellite")]
public class FireballSatelliteModData : EffectAttackModData {
    [SerializeField] private Explosioner fireExplosion;
    [SerializeField] private float radius;
    [SerializeField] private float damagePercent;
    [SerializeField] private float chance;

    public Explosioner FireExplosion { get => fireExplosion; }
    public float Radius { get => radius; }
    public float DamagePercent { get => damagePercent; }
    public float Chance { get => chance; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnableFireBallSatellite();
        character.ShipSkill.AddFireBallModInfo(new FireballSatelliteModInfor(this));
    }
}


public class FireballSatelliteModInfor : ModInfor<FireballSatelliteModData>, IModable {
    private FloatStat radius;
    private FloatStat damagePercent;

    public FloatStat Radius { get => radius; }
    public FloatStat DamagePercent { get => damagePercent; }

    public FireballSatelliteModInfor(FireballSatelliteModData mod) : base(mod) {
        radius = new FloatStat(mod.Radius);
        damagePercent = new FloatStat(mod.DamagePercent);
    }

    public FireballSatelliteModInfor(FireballSatelliteModInfor mod) : base(mod) {
        radius = new FloatStat(mod.radius);
        damagePercent = new FloatStat(mod.damagePercent);
    }


    public void FireBallBlast(ObjectBase causer, float damageBlast, Vector2 position) {
        if (RandomHelper.RandomWithPercent(modData.Chance)) {
            int damage = Mathf.RoundToInt(damageBlast * damagePercent.Value);
            float radiusValue = radius.Value;
            if (causer is ShipBase ship) {
                radiusValue *= (1 + ship.ShipStat.BlastRadiusPercent.Value);
            }
            else if (causer is DroneBase drone) {
                radiusValue *= (1 + drone.DroneStat.BlastRadiusPercent.Value);
            }
            Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(modData.FireExplosion, position);
            if (newExplosioner) {
                newExplosioner.SetHitInfor(damage, null, causer)
                            .SetRadius(radiusValue)
                            .Explosioning();
            }
        }
    }
    public object Clone() {
        return new FireballSatelliteModInfor(this);
    }

    public void ChangeRadius(StatModifier modifier) {
        this.radius.AddModifier(modifier);
    }

    public ModInfor GetModInfor() {
        return this;
    }
}