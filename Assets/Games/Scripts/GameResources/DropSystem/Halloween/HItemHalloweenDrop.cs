using Gemmob;
using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HItemHalloweenModeDrop", menuName = "Resource/HardData/Drop/Halloween/HItem")]
public class HItemHalloweenDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private TypeEnemyNumberHTicketDrop[] numberDropDatas;
    [SerializeField] private HItemDropController hItemPrefab;
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
            HalloweenModeWaveInfo waveInfo = GameManager.Instance.GetGameController<HalloweenModeController>().CurrentWaveInfo;
            if (waveInfo.RemainingHItem <= 0) {
                return;
            }

            TypeEnemyNumberHTicketDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }

            int numberDrop = numberDropData.NumberRange.GetRandomValue();
            numberDrop = waveInfo.GetHItemCanDrop(numberDrop);


            for (int i = 0; i < numberDrop; ++i) {
                DropHTicket(position);
                waveInfo.SpawnedHItem++;
            }
        }
    }

    private void DropHTicket(Vector2 position) {
        HItemDropController newHTicketController = GameManager.Instance.GameLoader.SpawnDropItem(hItemPrefab, position);
        if (newHTicketController) {
            newHTicketController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        hItemPrefab.RegisterPool(numberPreload);
    }
}

[Serializable]
public class TypeEnemyNumberHTicketDrop {
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private RangeIntValue numberRange;

    public EnemyType EnemyType { get => enemyType; }
    public RangeIntValue NumberRange { get => numberRange; }
}

