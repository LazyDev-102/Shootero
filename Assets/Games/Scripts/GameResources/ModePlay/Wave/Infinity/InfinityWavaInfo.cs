

using Helper;
using System.Collections.Generic;

public class InfinityWavaInfo {
    private InfinityWaveData waveData;
    private int limit;
    private int[] enemyIds;
    private int bossId;

    public InfinityWaveData WaveData { get => waveData; set => waveData = value; }
    public int Limit { get => limit; set => limit = value; }
    public int[] EnemyIds { get => enemyIds; set => enemyIds = value; }
    public int BossId { get => bossId; set => bossId = value; }

    public void CreateData(InfinityWaveData waveData) {
        this.WaveData = waveData;
        Limit = waveData.LimitRange.GetRandomValue();
        enemyIds = waveData.EnemyIds;
        int randomBossId;
        InfinityController controller = GameManager.Instance.GetGameController<InfinityController>();
        Queue<int> spawnedBossIds = controller.SpawnedBossIds;

        do {
            randomBossId = RandomHelper.RandomInCollection(waveData.BossIds);
        } while(spawnedBossIds.Contains(randomBossId));

        if(spawnedBossIds.Count == 2) {
            spawnedBossIds.Dequeue();
        }
        spawnedBossIds.Enqueue(randomBossId);
        bossId = randomBossId;
    }
}
