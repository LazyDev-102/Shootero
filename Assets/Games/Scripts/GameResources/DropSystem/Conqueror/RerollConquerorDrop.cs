using Gemmob;
using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RerollConquerorDrop", menuName = "Resource/HardData/Drop/Conqueror/Reroll")]
public class RerollConquerorDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private TypeEnemyNumberRerollDrop[] numberDropDatas;
    [SerializeField] private RerollDropController rerollPrefab;
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
            if (waveInfo.RemainingReroll <= 0) {
                return;
            }

            TypeEnemyNumberRerollDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }

            int numberDrop = numberDropData.NumberRange.GetRandomValue();
            numberDrop = waveInfo.GetRerollCanDrop(numberDrop);


            for (int i = 0; i < numberDrop; ++i) {
                DropReroll(position);
                waveInfo.SpawnedReroll++;
            }
        }
    }

    private void DropReroll(Vector2 position) {
        RerollDropController newRerollController = GameManager.Instance.GameLoader.SpawnDropItem(rerollPrefab, position);
        if (newRerollController) {
            newRerollController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        rerollPrefab.RegisterPool(numberPreload);
    }
}

[Serializable]
public class TypeEnemyNumberRerollDrop {
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private RangeIntValue numberRange;

    public EnemyType EnemyType { get => enemyType; }
    public RangeIntValue NumberRange { get => numberRange; }
}

