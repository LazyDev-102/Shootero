
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class GearModeWaveInfo {
    private GearModeWaveData waveData;
    private int limit;
    private int[] enemyIds;
    private int bossId;

    public GearModeWaveData WaveData { get => waveData; set => waveData = value; }
    public int Limit { get => limit; set => limit = value; }
    public int[] EnemyIds { get => enemyIds; set => enemyIds = value; }
    public int BossId { get => bossId; set => bossId = value; }

    public void CreateData(GearModeWaveData waveData) {
        this.WaveData = waveData;
        Limit = waveData.LimitRange.GetRandomValue();
        enemyIds = waveData.EnemyIds;
        int randomBossId;
        GearModeController controller = GameManager.Instance.GetGameController<GearModeController>();
        Queue<int> spawnedBossIds = controller.SpawnedBossIds;

        do {
            randomBossId = RandomHelper.RandomInCollection(waveData.BossIds);
        } while (spawnedBossIds.Contains(randomBossId));

        if (spawnedBossIds.Count == 2) {
            spawnedBossIds.Dequeue();
        }
        spawnedBossIds.Enqueue(randomBossId);
        bossId = randomBossId;
    }
}
