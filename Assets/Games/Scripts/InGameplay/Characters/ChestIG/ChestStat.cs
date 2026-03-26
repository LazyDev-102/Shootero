

using UnityEngine;

public class ChestStat : ObjectStat {
    private ChestBase chestBase;
    public ChestBase ChestBase {
        get {
            if (chestBase == null) {
                chestBase = ObjectBase as ChestBase;
            }
            return chestBase;
        }
    }

    [SerializeField] private int maxHPInit = 400;
    [SerializeField] private IntStat maxHp;
    public int MaxHPInit { get => maxHPInit; }
    public IntStat MaxHP { get => maxHp; set => maxHp = value; }
    public override void Initialize() {
        base.Initialize();
        maxHp.SetBaseValue(maxHPInit);
    }
}
