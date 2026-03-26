
using Gemmob;
using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialConquerorDrop", menuName = "Resource/HardData/Drop/Conqueror/MaterialDrop")]
public class MaterialConquerorDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private EnemyTypeNumberMaterialDrop[] numberDropDatas;
    [SerializeField] private ItemCollector materialCollector;
    [SerializeField] private ItemDropController materialDropPrefab;
    [SerializeField] private int numberPreload;
    public override void Droping(Vector2 position, EnemyType eType, int numberIcon) {

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
            if (waveInfo.RemainingMaterial <= 0) {
                return;
            }
            EnemyTypeNumberMaterialDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }
            int numberMaterialDrop = numberDropData.NumberRange.GetRandomValue();
            numberMaterialDrop = waveInfo.GetMaterialCanDrop(numberMaterialDrop);

            for (int i = 0; i < numberMaterialDrop; ++i) {
                Item itemRandom = RandomHelper.RandomInCollection(materialCollector.Items.Length - 1, materialCollector.Items);
                DropMaterial(position, itemRandom, 1);
                waveInfo.SpawnedMaterial++;
            }
        }
    }
    public override void Droping(Vector2 position, EnemyBase enemy) {
        Droping(position, enemy.Type);
    }

    private void DropMaterial(Vector2 position, Item item, int amount) {
        ItemDropController newMaterialController = GameManager.Instance.GameLoader.SpawnDropItem(materialDropPrefab, position);
        if (newMaterialController) {
            newMaterialController.SetItem(item, amount);
            newMaterialController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        materialDropPrefab.RegisterPool(numberPreload);
    }
}

[Serializable]
public class EnemyTypeNumberMaterialDrop {
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private RangeIntValue numberRange;

    public EnemyType EnemyType { get => enemyType; }
    public RangeIntValue NumberRange { get => numberRange; }
}

