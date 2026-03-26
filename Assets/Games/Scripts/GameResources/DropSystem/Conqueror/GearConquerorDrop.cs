
using Gemmob;
using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GearConquerorDrop", menuName = "Resource/HardData/Drop/Conqueror/GearDrop")]
public class GearConquerorDrop : BaseDrop {
    [SerializeField] private TypeEnemyDropProbability[] probabilityDropData;
    [SerializeField] private EnemyTypeNumberGearDrop[] numberDropDatas;
    [SerializeField] private ItemCollector gearCollector;
    [SerializeField] private GearDropController gearDropPrefab;
    [SerializeField] private int numberPreload;


    protected virtual void OnEnable() {
        EventDispatcher.Instance.AddListener<EventKey.OnEnoughDropPoint>(OnEnoughDropPoint);
    }

    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnEnoughDropPoint>(OnEnoughDropPoint);
    }
    protected virtual void OnEnoughDropPoint(EventKey.OnEnoughDropPoint infor) {
        Item itemRandom = RandomHelper.RandomInCollection(gearCollector.Items);
        DropGear(infor.Position, itemRandom);
    }
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
            if (waveInfo.RemainingGear <= 0) {
                return;
            }
            EnemyTypeNumberGearDrop numberDropData = null;
            foreach (var data in numberDropDatas) {
                if (data.EnemyType == eType) {
                    numberDropData = data;
                    break;
                }
            }
            int numberGearDrop = numberDropData.NumberRange.GetRandomValue();
            numberGearDrop = waveInfo.GetGearCanDrop(numberGearDrop);
            for (int i = 0; i < numberGearDrop; ++i) {
                Item itemRandom = RandomHelper.RandomInCollection(gearCollector.Items);
                DropGear(position, itemRandom);
                waveInfo.SpawnedGear++;
            }
        }
    }
    private void DropGear(Vector2 position, Item item) {
        GearDropController newGearController = GameManager.Instance.GameLoader.SpawnDropItem(gearDropPrefab, position);
        if (newGearController) {
            newGearController.SetItem(item);
            newGearController.Initalize();
        }
    }

    public override void PreloadOpenApp() {
        gearDropPrefab.RegisterPool(numberPreload);
    }

}

[Serializable]
public class EnemyTypeNumberGearDrop {
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private RangeIntValue numberRange;

    public EnemyType EnemyType { get => enemyType; }
    public RangeIntValue NumberRange { get => numberRange; }
}
