

using UnityEngine;

public class TrapStat : ObjectStat {
    private TrapBase trapBase;
    public TrapBase TrapBase {
        get {
            if (trapBase == null) {
                trapBase = ObjectBase as TrapBase;
            }
            return trapBase;
        }
    }

    [SerializeField] private int atkInit = 100;

    public int AtkInit { get => atkInit; }
}
