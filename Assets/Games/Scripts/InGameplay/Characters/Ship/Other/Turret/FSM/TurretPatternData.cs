using System;
using UnityEngine;

public abstract class TurretPatternData : ScriptableObject {
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damgePercent;
    [SerializeField] private int maxStackable;
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float DamgePercent { get => damgePercent; set => damgePercent = value; }
    public int MaxStackable { get => maxStackable; set => maxStackable = value; }
}

public class TurretPatternData<T> : TurretPatternData where T : TurretPatternInfor {
    [SerializeField] private T[] patternInfos;
    [SerializeField] private T[] focusPatternInfos;

    public T GetPatternByLevelIndex(int levelIndex) {
        if(levelIndex < 0 || levelIndex >= patternInfos.Length) {
            return null;
        }
        return patternInfos[levelIndex];
    }

    public T GetFocusPatternByLevelIndex(int levelIndex) {
        if(levelIndex < 0 || levelIndex >= focusPatternInfos.Length) {
            return null;
        }
        return focusPatternInfos[levelIndex];
    }
}

[Serializable]
public abstract class TurretPatternInfor {

}
