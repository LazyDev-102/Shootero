

using Helper;
using UnityEngine;

public class BossConquerorWaveInfo : ConquerorWaveInfo {
    private BossConquerorWaveData bossWaveData;
    private int chipInIcon;
    public BossConquerorWaveData BossWaveData { get => bossWaveData; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.bossWaveData = waveData as BossConquerorWaveData;
        chip = RandomHelper.RandomInRange(waveData.RangeChip);
        chipInIcon = this.bossWaveData.ChipInIcon;
        iconChip = Mathf.CeilToInt(1.0f * chip / chipInIcon);
    }

    //private int GetChipInWave() {
    //    int chipInIcon = GetChipInIcon();
    //    int randomChip = RandomHelper.RandomInRange(waveData.RangeChip);
    //    randomChip = (randomChip / chipInIcon) * chipInIcon;
    //    if (randomChip < waveData.RangeChip.startValue) {
    //        randomChip = (randomChip / chipInIcon + 1) * chipInIcon;
    //    }
    //    return randomChip;
    //}

    public int GetBossId() {
        return RandomHelper.RandomInCollection(bossWaveData.BossIds);
    }

    public override int GetChipInIcon() {
        return chipInIcon;
    }

    public override float GetWaveMultipler() {
        return bossWaveData.WaveMultipler;
    }

    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<BossConquerorWaveSpawner>("Wave Spawner");
        }
        BossConquerorWaveSpawner newSpawner = spawner.GetComponent<BossConquerorWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<BossConquerorWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }
}
