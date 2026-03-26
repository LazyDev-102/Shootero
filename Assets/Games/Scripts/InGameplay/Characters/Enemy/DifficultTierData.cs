using UnityEngine;

[CreateAssetMenu(fileName = "DifficultTierData", menuName = "Resource/HardData/Tier/DifficultTierData")]
public class DifficultTierData : ScriptableObject {
    [SerializeField] private DifficultSpawnEnemy[] enemyPercent;
    [SerializeField] private DifficultSpawnTrap[] trapPercent;

    public DifficultSpawnEnemy[] EnemyPercent { get => enemyPercent; }
    public DifficultSpawnTrap[] TrapPercent { get => trapPercent; }
}
