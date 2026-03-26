using UnityEngine;
using System;
using Helper;
using Gemmob;

[CreateAssetMenu(fileName = "ChipConquerorDrop", menuName = "Resource/HardData/Drop/Conqueror/ChipDrop")]
public class ChipConquerorDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private TypeEnemyNumberIconDrop[] numberIconDropDatas;
    [SerializeField] private ChipDropController iconPrefab;
    [SerializeField] private int numberPreload;

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
            if (waveInfo.RemainingIcon <= 0) {
                return;
            }
            TypeEnemyNumberIconDrop numberIconDropData = null;
            foreach (var data in numberIconDropDatas) {
                if (data.Type == eType) {
                    numberIconDropData = data;
                    break;
                }
            }
            int numberChipInIcon = waveInfo.GetChipInIcon();
            int numberIconDrop = numberIconDropData.IconRange.GetRandomValue();
            numberIconDrop = waveInfo.GetIconCanDrop(numberIconDrop);

            for (int i = 0; i < numberIconDrop; ++i) {
                DropIcon(position, numberChipInIcon);
                waveInfo.SpawnedIcon++;
            }
        }
    }
    public override void Droping(Vector2 position, EnemyType eType, int numberIcon) {
        ConquerorWaveInfo waveInfo = GameManager.Instance.GetGameController<ConquerorController>().CurrentWaveInfo;
        int numberChipInIcon = waveInfo.GetChip(eType, numberIcon);

        for (int i = 0; i < numberIcon; ++i) {
            DropIcon(position, numberChipInIcon);
        }
    }
    private void DropIcon(Vector2 position, int numberChip) {
        ChipDropController newChipController = GameManager.Instance.GameLoader.SpawnDropItem(iconPrefab, position);
        if (newChipController) {
            newChipController.SetChip(numberChip);
            newChipController.Initalize();
            GameManager.Instance.SetDropStatus(true);
        }
    }
    public override void PreloadOpenApp() {
        iconPrefab.RegisterPool(numberPreload);
    }

}

[Serializable]
public class TypeEnemyNumberIconDrop {
    [SerializeField] private EnemyType type;
    [SerializeField] private RangeIntValue iconRange;

    public EnemyType Type { get => type; set => type = value; }
    public RangeIntValue IconRange { get => iconRange; set => iconRange = value; }
}


