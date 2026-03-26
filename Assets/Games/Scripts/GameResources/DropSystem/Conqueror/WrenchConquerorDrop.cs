using Gemmob;
using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WrenchConquerorDrop", menuName = "Resource/HardData/Drop/Conqueror/WrenchDrop")]
public class WrenchConquerorDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private TypeWrenchValue[] valueWrenchs;
    [SerializeField] private TypeEnemyNumberWernchDrop[] numberDropDatas;
    [SerializeField] private WrenchDropController wranchPrefab;
    [SerializeField] private int numberPreload;
    public override void Droping(Vector2 position, EnemyType eType, int numberIcon) {

    }
    public override void Droping(Vector2 position, EnemyBase enemy) {
        Droping(position, enemy.Type);
    }
    public override void Droping(Vector2 position, EnemyType eType) {
        TypeEnemyDropProbability dropProbability = null;
        foreach (var data in probabilityDropData) {
            if (data.Type == eType) {
                dropProbability = data;
                break;
            }
        }
        if (RandomHelper.RandomWithProbability(dropProbability.Probability)) {
            ConquerorWaveInfo waveInfo = GameManager.Instance.GetGameController<ConquerorController>().CurrentWaveInfo;
            if (waveInfo.RemainingWrench <= 0) {
                return;
            }

            TypeEnemyNumberWernchDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }
            TypeWrenchValue value = null;
            foreach (var data in valueWrenchs) {
                if (data.Type == numberDropData.WrenchType) {
                    value = data;
                    break;
                }
            }

            int numberDrop = numberDropData.NumberRange.GetRandomValue();
            numberDrop = waveInfo.GetWrenchCanDrop(numberDrop);


            for (int i = 0; i < numberDrop; ++i) {
                DropWrench(position, value);
                waveInfo.SpawnedWrench++;
            }
        }
    }

    private void DropWrench(Vector2 position, TypeWrenchValue value) {
        WrenchDropController newWrenchController = GameManager.Instance.GameLoader.SpawnDropItem(wranchPrefab, position);
        if (newWrenchController) {
            newWrenchController.SetHp(value.Value);
            newWrenchController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        wranchPrefab.RegisterPool(numberPreload);
    }
}

[Serializable]
public class TypeWrenchValue {
    [SerializeField] private WrenchType type;
    [SerializeField] private float value;
    [SerializeField] private float size;

    public WrenchType Type { get => type; }
    public float Value { get => value; }
    public float Size { get => size; }
}

[Serializable]
public class TypeEnemyNumberWernchDrop {
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private WrenchType wrenchType;
    [SerializeField] private RangeIntValue numberRange;

    public EnemyType EnemyType { get => enemyType; }
    public WrenchType WrenchType { get => wrenchType; }
    public RangeIntValue NumberRange { get => numberRange; }
}

public enum WrenchType {
    Small, Normal, Huge
}

