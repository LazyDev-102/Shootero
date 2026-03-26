
using Gemmob;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "WrenchBossModeDrop", menuName = "Resource/HardData/Drop/BossMode/WrenchDrop")]
public class WrenchBossModeDrop : BaseDrop {
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


            for (int i = 0; i < numberDrop; ++i) {
                DropWrench(position, value);
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
