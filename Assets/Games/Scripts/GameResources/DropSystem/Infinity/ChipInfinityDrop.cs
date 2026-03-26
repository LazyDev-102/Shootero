
using Gemmob;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipInfinityDrop", menuName = "Resource/HardData/Drop/Infinity/ChipDrop")]
public class ChipInfinityDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private TypeEnemyNumberIconDrop[] numberIconDropDatas;
    [SerializeField] private ChipDropController iconPrefab;
    [SerializeField] private int numberPreload;
    public override void Droping(Vector2 position, EnemyType eType, int numberIcon) {

    }
    public override void Droping(Vector2 position, EnemyBase enemy) {
        Droping(position, enemy.Type);
    }

    private void DropIcon(Vector2 position, int numberChip) {
        ChipDropController newChipController = GameManager.Instance.GameLoader.SpawnDropItem(iconPrefab, position);
        if (newChipController) {
            newChipController.SetChip(numberChip);
            newChipController.Initalize();
        }
    }

    private int GetChipInIcon(float infinityMultipler) {
        return Mathf.CeilToInt(5 * (0.8f + 0.2f * infinityMultipler));
    }

    public override void PreloadOpenApp() {
        iconPrefab.RegisterPool(numberPreload);
    }

    public override void Droping(Vector2 position, EnemyType eType) {
        TypeEnemyDropProbability dropProbablity = null;
        foreach (var data in probabilityDropData) {
            if (data.Type == eType) {
                dropProbablity = data;
                break;
            }
        }
        if (RandomHelper.RandomWithProbability(dropProbablity.Probability)) {
            InfinityController controller = GameManager.Instance.GetGameController<InfinityController>();
            TypeEnemyNumberIconDrop numberIconDropData = null;
            foreach (var data in numberIconDropDatas) {
                if (data.Type == eType) {
                    numberIconDropData = data;
                    break;
                }
            }
            int numberChipInIcon = GetChipInIcon(controller.CurrentInfinityMultipler);
            int numberIconDrop = numberIconDropData.IconRange.GetRandomValue();

            for (int i = 0; i < numberIconDrop; ++i) {
                DropIcon(position, numberChipInIcon);
            }
        }
    }
}
