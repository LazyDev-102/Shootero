

using System;
using UnityEngine;

public class T01Stat : TrapStat {
    private T01Base t01Base;
    public T01Base T01Base {
        get {
            if(t01Base == null) {
                t01Base = TrapBase as T01Base;
            }
            return t01Base;
        }
    }

    [SerializeField] private FloatStat moveSpeed;

    public FloatStat MoveSpeed { get => moveSpeed; }
}
