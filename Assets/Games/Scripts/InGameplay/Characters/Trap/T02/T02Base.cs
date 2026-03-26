

using UnityEngine;
using System.Collections.Generic;
using Gemmob;

public class T02Base : TrapBase {
    #region
    private T02Attack t02Attack;
    public T02Attack T02Attack {
        get {
            if (t02Attack == null) {
                t02Attack = TrapAttack as T02Attack;
            }
            return t02Attack;
        }
    }

    private T02Move t02Move;
    public T02Move T02Move {
        get {
            if (t02Move == null) {
                t02Move = TrapMove as T02Move;
            }
            return t02Move;
        }
    }

    private T02Stat t02Stat;
    public T02Stat T02Stat {
        get {
            if (t02Stat == null) {
                t02Stat = TrapStat as T02Stat;
            }
            return t02Stat;
        }
    }

    private T02Hitbox t02Hitbox;
    public T02Hitbox T02Hitbox {
        get {
            if (t02Hitbox == null) {
                t02Hitbox = TrapHitbox as T02Hitbox;
            }
            return t02Hitbox;
        }
    }
    #endregion
    [SerializeField] protected T02Laser laserPrefab;
    [SerializeField] protected int numberLaser;
    [SerializeField] protected float laserAtkPercent;
    [SerializeField] protected float baseAtkPercent;
    [SerializeField] private int numberPreload;

    private List<T02Laser> verticalTrap02es = new List<T02Laser>();
    private List<T02Laser> horizontalTrap02es = new List<T02Laser>();


    public override void PreloadIngame() {
        base.PreloadIngame();
        if (laserPrefab) {
            laserPrefab.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        base.Initialize();
        //verticalTrap02es.Clear();
        //horizontalTrap02es.Clear();
    }

    public override void Spawn() {
        for (int i = 0; i < numberLaser; ++i) {
            T02Laser newLaser = laserPrefab.Spawn(transform);
            newLaser.SetT02Base(this);
            newLaser.SetHitInfo((int)(T02Stat.Atk.Value * laserAtkPercent), null, this);
            newLaser.SetBaseHitInfo((int)(T02Stat.Atk.Value * baseAtkPercent), null, this);
            newLaser.Spawn(spawnBorderType, spawnBorderOffset);
        }
    }

    public override void Updating() {
        base.Updating();
        foreach (var t in verticalTrap02es) {
            t.Updating();
        }

        foreach (var t in horizontalTrap02es) {
            t.Updating();
        }
    }


    public bool CheckPositionT02(T02Laser t02) {
        bool isVertical = t02.IsVertical;
        List<T02Laser> t02es = isVertical ? verticalTrap02es : horizontalTrap02es;
        foreach (var trap in t02es) {
            if (t02.CheckConflicPositionSpawn(trap)) {
                return false;
            }
        }
        return true;
    }

    public void AddT02(T02Laser t02) {
        if (t02.IsVertical) {
            verticalTrap02es.Add(t02);
        }
        else {
            horizontalTrap02es.Add(t02);
        }
    }

    public void RemoveT02Laser(T02Laser t02laser) {
        if (t02laser.IsVertical) {
            verticalTrap02es.Remove(t02laser);
        }
        else {
            horizontalTrap02es.Remove(t02laser);
        }

        if (verticalTrap02es.Count == 0 && horizontalTrap02es.Count == 0) {
            Despawn();
        }
    }
}
