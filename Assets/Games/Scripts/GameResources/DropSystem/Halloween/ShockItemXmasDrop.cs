using Gemmob;
using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShockItemXmasDrop", menuName = "Resource/HardData/Drop/Xmas/ShockItem")]
public class ShockItemXmasDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private TypeEnemyNumberHTicketDrop[] numberDropDatas;
    [SerializeField] private ShockItemDropController shockItemPrefab;
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
            XmasModeWaveInfo waveInfo = GameManager.Instance.GetGameController<XmasModeController>().CurrentWaveInfo;
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
        ShockItemDropController newHTicketController = GameManager.Instance.GameLoader.SpawnDropItem(shockItemPrefab, position);
        if (newHTicketController) {
            newHTicketController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        shockItemPrefab.RegisterPool(numberPreload);
    }
}

