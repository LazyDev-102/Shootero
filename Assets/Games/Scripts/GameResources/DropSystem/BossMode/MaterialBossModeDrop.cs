
using Gemmob;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialBossModeDrop", menuName = "Resource/HardData/Drop/BossMode/MaterialDrop")]
public class MaterialBossModeDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private EnemyTypeNumberMaterialDrop[] numberDropDatas;
    [SerializeField] private ItemCollector materialCollector;
    [SerializeField] private ItemDropController materialDropPrefab;
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
            EnemyTypeNumberMaterialDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }
            int numberMaterialDrop = numberDropData.NumberRange.GetRandomValue();

            for (int i = 0; i < numberMaterialDrop; ++i) {
                DropMaterial(position, 1, 1);
            }
        }
    }

    private void DropMaterial(Vector2 position, int id, int amount) {
        Item itemRandom = RandomHelper.RandomInCollection(materialCollector.Items.Length - 1, materialCollector.Items);
        ItemDropController newWrenchController = GameManager.Instance.GameLoader.SpawnDropItem(materialDropPrefab, position);
        if (newWrenchController) {
            newWrenchController.SetItem(itemRandom, amount);
            newWrenchController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        materialDropPrefab.RegisterPool(numberPreload);
    }
}
