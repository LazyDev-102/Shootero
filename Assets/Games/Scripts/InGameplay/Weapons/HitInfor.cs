using System.Collections.Generic;
public class HitInfor {
    private IntStat damage;
    private List<IEffectAttackModable> effects = new List<IEffectAttackModable>();
    private ObjectBase causer;
    private int critChance;
    private float critDamage;

    public IntStat Damage {
        get {
            if (damage == null) {
                damage = new IntStat();
            }
            return damage;
        }
    }

    public int CritChance { get => critChance; }
    public float CritDamage { get => critDamage; }

    public List<IEffectAttackModable> Effects { get => effects; }
    public ObjectBase Causer { get => causer; }

    public HitInfor() {
        damage = new IntStat();
        effects = new List<IEffectAttackModable>();
        causer = null;
    }

    public void SetInfor(int damageBase, List<IEffectAttackModable> effects, ObjectBase causer, int critChance = 0, float critDamage = 0) {
        Damage.Reset();
        Damage.SetBaseValue(damageBase);
        this.effects = effects;
        this.causer = causer;
        this.critChance = critChance;
        this.critDamage = critDamage;
    }

}

