
using Helper;
using UnityEngine;

public class HalloweenModeWaveInfo : ConquerorWaveInfo { // softData
    private HalloweenModeWaveData halloweenWaveData;
    private int limit;
    private int time;
    private int currentWaveIndex;
    private int chipInIcon;

    protected int numberHItem;
    protected int spawnedHItem;

    public int NumberHItem { get => numberHItem; }
    public int SpawnedHItem { get => spawnedHItem; set => spawnedHItem = value; }
    public int RemainingHItem { get => (numberHItem - spawnedHItem); }


    public HalloweenModeWaveData HalloweenWaveData { get => halloweenWaveData; }
    public int Limit { get => limit; }
    public int Time { get => time; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(0, currentWaveIndex, waveData);
        this.halloweenWaveData = waveData as HalloweenModeWaveData;
        this.currentWaveIndex = currentWaveIndex;
        ConfigModeWave configModeWaves = this.halloweenWaveData.ConfigModeWaves;

        limit = RandomHelper.RandomInRange(configModeWaves.LimitRange);
        time = RandomHelper.RandomInRange(configModeWaves.TimeRange);
        chip = RandomHelper.RandomInRange(waveData.RangeChip);
        chipInIcon = Mathf.CeilToInt(chip / time);
        if (chipInIcon == 0)
            chipInIcon = 1;
        iconChip = Mathf.CeilToInt(1.0f * chip / chipInIcon);
        numberHItem = halloweenWaveData.RangeHItem.GetRandomValue();
    }

    public override int GetChipInIcon() {
        return chipInIcon;
    }

    public int GetHItemCanDrop(int numberDrop) {
        return Mathf.Min(numberDrop, RemainingHItem);
    }

    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<HalloweenModeWaveSpawner>("Wave Spawner");
        }
        HalloweenModeWaveSpawner newSpawner = spawner.GetComponent<HalloweenModeWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<HalloweenModeWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }

    public override float GetWaveMultipler() {
        return halloweenWaveData.WaveMultipler;
    }

    public int GetBossId() {
        return RandomHelper.RandomInCollection(HalloweenWaveData.BossIds);
    }
    public int GetMiniBossId() {
        return RandomHelper.RandomInCollection(HalloweenWaveData.MinibossIds);
    }

}
