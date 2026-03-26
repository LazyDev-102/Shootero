using System;
using UnityEngine;

public abstract class ShipPatternData : ScriptableObject {
    public abstract float GetDamagePercent(int currentBulletupIndex);
    public abstract float GetFocusDamagePercent(int currentBulletupIndex);

    public abstract float GetAttackSpeed(int currentBulletupIndex);
    public abstract float GetFocusAttackSpeed(int currentBulletupIndex);


}

public class ShipPatternData<T> : ShipPatternData where T : ShipPatternInfo {
    [SerializeField] protected T[] patternInfos;
    [SerializeField] protected T[] focusPatternInfos;

    public T GetPatternByLevelIndex(int levelIndex) {
        if (levelIndex < 0 || levelIndex >= patternInfos.Length) {
            return null;
        }
        return patternInfos[levelIndex];
    }

    public T GetFocusPatternByLevelIndex(int levelIndex) {
        if (levelIndex < 0 || levelIndex >= focusPatternInfos.Length) {
            return null;
        }
        return focusPatternInfos[levelIndex];
    }

    public override float GetDamagePercent(int currentBulletupIndex) {
        return patternInfos[currentBulletupIndex].DamagePercent;
    }

    public override float GetFocusDamagePercent(int currentBulletupIndex) {
        return focusPatternInfos[currentBulletupIndex].DamagePercent;
    }

    public override float GetAttackSpeed(int currentBulletupIndex) {
        return patternInfos[currentBulletupIndex].AttackSpeed;
    }

    public override float GetFocusAttackSpeed(int currentBulletupIndex) {
        return focusPatternInfos[currentBulletupIndex].AttackSpeed;
    }
}

[Serializable]
public abstract class ShipPatternInfo {
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damagePercent;
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float DamagePercent { get => damagePercent; set => damagePercent = value; }
}
