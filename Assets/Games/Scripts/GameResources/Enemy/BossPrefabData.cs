using UnityEngine;

[CreateAssetMenu(fileName = "BossPrefabData", menuName = "Resource/HardData/EnemyData/BossPrefabData")]
public class BossPrefabData : ScriptableObject {
    [SerializeField] private BossData[] bosses;

    public BossData[] Bosses { get => bosses; }

    public BossBase GetBoss(int index) {
        return bosses[index].bossBase;
    }
    public Color GetBossBGColor(int index) {
        return bosses[index].bossBG;
    }

    [System.Serializable]
    public class BossData {
        public Color bossBG;
        public BossBase bossBase;
    }
}
