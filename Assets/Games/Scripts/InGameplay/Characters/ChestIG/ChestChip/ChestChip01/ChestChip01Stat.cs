

using System;
using UnityEngine;

public class ChestChip01Stat : ChestStat {
    private ChestChip01Base chestChip01Base;
    public ChestChip01Base ChestChip01Base {
        get {
            if (chestChip01Base == null) {
                chestChip01Base = ChestBase as ChestChip01Base;
            }
            return chestChip01Base;
        }
    }

    [SerializeField] private FloatStat moveSpeed;

    public FloatStat MoveSpeed { get => moveSpeed; }
}
