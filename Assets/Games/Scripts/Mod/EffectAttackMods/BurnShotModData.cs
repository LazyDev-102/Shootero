using UnityEngine;

[CreateAssetMenu(fileName = "BurnShotModData", menuName = "Mod/EffectAttack/BurnShot")]
public class BurnShotModData : EffectAttackModData {
    [SerializeField] private float duration;
    [SerializeField] private float deltaBurn;
    [SerializeField] private float damagePercent;

    public float Duration { get => duration; }
    public float DeltaBurn { get => deltaBurn; }
    public float DamagePercent { get => damagePercent; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        BurnShotModInfor burnInfor = new BurnShotModInfor(this);
        character.ShipSkill.AddEffectAttackMod(burnInfor);
    }
}


public class BurnShotModInfor : EffectAttackModInfor<BurnShotModData> {
    private FloatStat duration;
    private FloatStat deltaBurn;
    private FloatStat damagePercent;
    private int maxBurnStack;


    public BurnShotModInfor(BurnShotModData mod) : base(mod) {
        duration = new FloatStat(mod.Duration);
        deltaBurn = new FloatStat(mod.DeltaBurn);
        damagePercent = new FloatStat(mod.DamagePercent);
        maxBurnStack = 1;
    }

    protected BurnShotModInfor(BurnShotModInfor mod) : base(mod) {
        duration = new FloatStat(mod.duration);
        deltaBurn = new FloatStat(mod.deltaBurn);
        damagePercent = new FloatStat(mod.damagePercent);
        maxBurnStack = mod.maxBurnStack;
    }

    public FloatStat Duration { get => duration; }
    public FloatStat DeltaBurn { get => deltaBurn; }
    public FloatStat DamagePercent { get => damagePercent; }
    public int MaxBurnStack { get => maxBurnStack; set => maxBurnStack = value; }


    public override object Clone() {
        return new BurnShotModInfor(this);
    }

    public override void EffectTo(CharacterBase victim, ObjectBase causer, IntStat damageStat, Vector2 position) {
        if (victim == null || victim.CharacterSkill == null || damageStat == null)
            return;
        int damage = Mathf.RoundToInt(damageStat.Value * damagePercent.Value);
        BurnEffect effect = new BurnEffect(victim, causer, duration.Value, deltaBurn.Value, damage, maxBurnStack);
        victim.CharacterSkill.AddCountdownEffect(effect);
    }
}