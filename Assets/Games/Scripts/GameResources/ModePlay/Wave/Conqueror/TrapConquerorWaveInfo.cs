using Helper;
using UnityEngine;

public class TrapConquerorWaveInfo : ConquerorWaveInfo { // softData
    private TrapConquerorWaveData trapWaveData;
    private int time;
    private int currentZoneIndex;
    private int currentWaveIndex;
    private int chipInIcon;


    public TrapConquerorWaveData TrapWaveData { get => trapWaveData; }
    public int WaveTime { get => time; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.trapWaveData = waveData as TrapConquerorWaveData;
        this.currentWaveIndex = currentWaveIndex;
        this.currentZoneIndex = currentZoneIndex;

        time = RandomHelper.RandomInRange(this.trapWaveData.WaveTime);
        chip = RandomHelper.RandomInRange(this.trapWaveData.RangeChip);
    }

    public override int GetChipInIcon() {
        return chipInIcon;// waveData.GetChip();
    }
    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<TrapConquerorWaveSpawner>("Wave Spawner");
        }
        TrapConquerorWaveSpawner newSpawner = spawner.GetComponent<TrapConquerorWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<TrapConquerorWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }

    public override float GetWaveMultipler() {
        return trapWaveData.WaveMultipler;
    }


}
