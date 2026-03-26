
using Gemmob;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "GearInfinityDrop", menuName = "Resource/HardData/Drop/Infinity/GearDrop")]
public class GearInfinityDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabiltyDropData;
    [SerializeField] private EnemyTypeNumberGearDrop[] numberDropDatas;
    [SerializeField] private ItemCollector gearCollector;
    [SerializeField] private GearDropController gearDropPrefab;
    [SerializeField] private int numberPreload;
    public override void Droping(Vector2 position, EnemyType eType, int numberIcon) {

    }
    public override void Droping(Vector2 position, EnemyType eType) {
        TypeEnemyDropProbability dropProbability = null;
        foreach (var data in probabiltyDropData) {
            if (data.Type == eType) {
                dropProbability = data;
                break;
            }
        }
        if (RandomHelper.RandomWithProbability(dropProbability.Probability)) {
            EnemyTypeNumberGearDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }
            int numberGearDrop = numberDropData.NumberRange.GetRandomValue();

            for (int i = 0; i < numberGearDrop; ++i) {
                DropMaterial(position);
            }
        }
    }
    public override void Droping(Vector2 position, EnemyBase enemy) {
        Droping(position, enemy.Type);
    }

    private void DropMaterial(Vector2 position) {
        Item itemRandom = RandomHelper.RandomInCollection(gearCollector.Items);
        GearDropController newWrenchController = GameManager.Instance.GameLoader.SpawnDropItem(gearDropPrefab, position);
        if (newWrenchController) {
            newWrenchController.SetItem(itemRandom);
            newWrenchController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        gearDropPrefab.RegisterPool(numberPreload);
    }
}
