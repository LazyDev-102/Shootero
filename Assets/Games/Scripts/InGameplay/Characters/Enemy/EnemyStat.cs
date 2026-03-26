using UnityEngine;

public class EnemyStat : CharacterStat {
    private EnemyBase enemyBase;
    public EnemyBase EnemyBase {
        get {
            if (enemyBase == null) {
                enemyBase = CharacterBase as EnemyBase;
            }
            return enemyBase;
        }
    }

    [SerializeField] protected FloatStat size;


    [SerializeField] private int maxHPInit = 400;
    [SerializeField] private int atkInit = 100;


    public int MaxHPInit { get => maxHPInit; }
    public int AtkInit { get => atkInit; }
    public FloatStat Size { get => size; }


    public override void Destroy() {
        base.Destroy();
        size.SetBaseValue(1, true);
    }
#if UNITY_EDITOR
    [SerializeField] EnemyStat reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        atkInit = reference.atkInit;
        maxHPInit = reference.maxHPInit;
        Atk.SetBaseValue(reference.Atk.Value, true);
        MaxHP.SetBaseValue(reference.MaxHP.Value, true);
        ColliderDamage.SetBaseValue(reference.ColliderDamage.Value, true);
        Size.SetBaseValue(reference.Size.Value, true);
    }
#endif
}
