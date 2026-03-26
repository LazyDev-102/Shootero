using UnityEngine;
using System;
using Helper;
using System.Collections.Generic;
using Gemmob;
[RequireComponent(typeof(ChestChip01Attack), typeof(ChestChip01Move), typeof(ChestChip01Health))]
[RequireComponent(typeof(ChestChip01Hitbox), typeof(ChestChip01Stat), typeof(ChestChip01StateController))]
[RequireComponent(typeof(ChestChip01Effect))]

public class ChestChip01Base : ChestBase {
    #region
    private ChestChip01Attack chestChip01Attack;
    public ChestChip01Attack ChestChip01Attack {
        get {
            if (chestChip01Attack == null) {
                chestChip01Attack = ChestAttack as ChestChip01Attack;
            }
            return chestChip01Attack;
        }
    }

    private ChestChip01Move chestChip01Move;
    public ChestChip01Move ChestChip01Move {
        get {
            if (chestChip01Move == null) {
                chestChip01Move = ChestMove as ChestChip01Move;
            }
            return chestChip01Move;
        }
    }

    private ChestChip01Stat chestChip01Stat;
    public ChestChip01Stat ChestChip01Stat {
        get {
            if (chestChip01Stat == null) {
                chestChip01Stat = ChestStat as ChestChip01Stat;
            }
            return chestChip01Stat;
        }
    }

    private ChestChip01Hitbox chestChip01Hitbox;
    public ChestChip01Hitbox ChestChip01Hitbox {
        get {
            if (chestChip01Hitbox == null) {
                chestChip01Hitbox = ChestHitbox as ChestChip01Hitbox;
            }
            return chestChip01Hitbox;
        }
    }
    #endregion
    [SerializeField] private int numberPreload;

    public override void PreloadIngame() {
        base.PreloadIngame();
    }

    public override void Initialize() {
        base.Initialize();
    }

    public void SpawnParts() {
    }
    public override void Destroy() {
        base.Destroy();
    }
}
