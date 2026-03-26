using UnityEngine;

[CreateAssetMenu(fileName = "ConquerorDropSystem", menuName = "Resource/HardData/Drop/ConquerorDropSystem")]
public class ConquerorDropSystem : ScriptableObject {
    [SerializeField] private BaseDrop[] conquerorDrops;
    [SerializeField] private BaseDrop conquerorChipDrop;

    public void Droping(Vector2 position, EnemyBase enemy) {
        foreach (var drop in conquerorDrops) {
            drop.Droping(position, enemy);
        }
    }

    public void DropingChip(Vector2 position, EnemyBase enemy) {
        conquerorChipDrop.Droping(position, enemy);
    }
    public void DropingChip(Vector2 position, EnemyType eType) {
        conquerorChipDrop.Droping(position, eType);
    }

    public void DropingChip(Vector2 position, EnemyType eType, int numberChipFake) {
        conquerorChipDrop.Droping(position, eType, numberChipFake);
    }

    public void PreloadOpenApp() {
        foreach (var drop in conquerorDrops) {
            drop.PreloadOpenApp();
        }
        conquerorChipDrop.PreloadOpenApp();
    }
}


[System.Serializable]
public class TypeEnemyDropProbability {
    [SerializeField] private EnemyType type;
    [SerializeField] private int percent;

    public EnemyType Type { get => type; }
    public int Probability { get => percent; }
}