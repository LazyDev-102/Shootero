using Helper;
using Gemmob;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ZoneEnemies", menuName = "Resource/HardData/EnemyData/ZoneEnemy")]
public class ZoneEnemies : ScriptableObject {
    [SerializeField] private EnemyClassification[] enemies;

    public EnemyClassification[] Enemies { get => enemies; }

    public void Preload(bool isLoadAll) {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].Preload(isLoadAll);
        }
    }
    public void Preload(int[] enemyIds) {
        for (int i = 0; i < enemies.Length; i++) {
            if (enemyIds.Contains(i + 1))
                enemies[i].Preload(true);
        }
    }

    public EnemyBase GetEnemyBaseRandom(int[] indexs, EnemyType type) {
        EnemyClassification species = enemies[RandomHelper.RandomInCollection(indexs) - 1];
        foreach (EnemyBase enemyBase in species.E) {
            if (enemyBase.Type == type) {
                return enemyBase;
            }
        }
        return null;
    }

    public EnemyBase GetEnemyBaseRandom(int[] indexs, EnemyType type, int maxLength) {
        EnemyClassification species = enemies[RandomHelper.RandomInCollection(maxLength, indexs) - 1];
        foreach (EnemyBase enemyBase in species.E) {
            if (enemyBase.Type == type) {
                return enemyBase;
            }
        }
        return null;
    }

    [System.Serializable]
    public class EnemyClassification {
        [SerializeField] private bool active;
        [SerializeField] private EnemyBase[] e;

        public EnemyBase[] E { get => e; }

        public void Preload(bool isLoadAll) {
            if (e != null) {
                if (isLoadAll || active)
                    for (int i = 0; i < e.Length; i++) {
                        e[i].PreloadIngame();
                        e[i].RegisterPool(10);
                    }
            }
        }
    }
}
