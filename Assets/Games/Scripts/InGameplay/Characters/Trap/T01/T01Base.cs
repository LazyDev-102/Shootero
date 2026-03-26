using UnityEngine;
using System;
using Helper;
using System.Collections.Generic;
using Gemmob;

public class T01Base : TrapBase {
    #region
    private T01Attack t01Attack;
    public T01Attack T01Attack {
        get {
            if (t01Attack == null) {
                t01Attack = TrapAttack as T01Attack;
            }
            return t01Attack;
        }
    }

    private T01Move t01Move;
    public T01Move T01Move {
        get {
            if (t01Move == null) {
                t01Move = TrapMove as T01Move;
            }
            return t01Move;
        }
    }

    private T01Stat t01Stat;
    public T01Stat T01Stat {
        get {
            if (t01Stat == null) {
                t01Stat = TrapStat as T01Stat;
            }
            return t01Stat;
        }
    }

    private T01Hitbox t01Hitbox;
    public T01Hitbox T01Hitbox {
        get {
            if (t01Hitbox == null) {
                t01Hitbox = TrapHitbox as T01Hitbox;
            }
            return t01Hitbox;
        }
    }
    #endregion
    [SerializeField] private T01PartRandomSize[] randomSizes;
    [SerializeField] private int numberPart;
    [SerializeField] private float randomRadius = 2;
    [SerializeField] private int numberPreload;


    private List<T01PartBase> parts = new List<T01PartBase>();
    private List<T01PartBase> partTriggered = new List<T01PartBase>();

    public override void PreloadIngame() {
        base.PreloadIngame();
        foreach (var p in randomSizes) {
            p.PartPrefab.PreloadIngame();
            p.PartPrefab.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        parts.Clear();
        partTriggered.Clear();
        base.Initialize();
    }

    public void SpawnParts() {
        for (int i = 0; i < numberPart; ++i) {
            T01PartRandomSize random = RandomHelper.RandomWithPercent(randomSizes);
            T01PartBase partPrefab = random.PartPrefab;
            Vector2 positionSpawn = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * randomRadius;
            T01PartBase newPart = partPrefab.Spawn(transform, positionSpawn);
            newPart.T01PartStat.Atk.SetBaseValue(T01Stat.Atk.Value, true);
            newPart.SetParent(this);
            newPart.Initialize();
            parts.Add(newPart);
        }
    }
    public void ReloadChildDamage(T01PartBase child) {
        partTriggered.Add(child);
        foreach (var item in parts) {
            if (item == null || partTriggered.Contains(item))
                continue;
            if (item.T01PartStat != null)
                item.T01PartStat.Atk.SetBaseValue(item.T01PartStat.Atk.Value / 2, true);
        }
    }
    public override void Destroy() {
        foreach (var part in parts) {
            if (part != null)
                part.Recycle();
        }
        foreach (var part in partTriggered) {
            if (part != null)
                part.Recycle();
        }
        parts.Clear();
        partTriggered.Clear();
        base.Destroy();
    }

    [Serializable]
    public class T01PartRandomSize : IPercentable {
        [SerializeField] private T01PartBase partPrefab;
        [SerializeField] private int percent;

        public T01PartBase PartPrefab { get => partPrefab; }

        public int GetPercent() {
            return percent;
        }
    }
}
