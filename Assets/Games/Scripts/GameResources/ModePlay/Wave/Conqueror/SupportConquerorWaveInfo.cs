
using UnityEngine;

public class SupportConquerorWaveInfo : ConquerorWaveInfo {
    private SupportConquerorWaveData supportWaveData;
    public SupportConquerorWaveData SupportWaveData { get => supportWaveData; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.supportWaveData = waveData as SupportConquerorWaveData;
    }

    public override int GetChipInIcon() {
        return 0;
    }

    public override float GetWaveMultipler() {
        return supportWaveData.WaveMultipler;
    }

    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<SupportConquerorWaveSpawner>("Wave Spawner");
        }
        SupportConquerorWaveSpawner newSpawner = spawner.GetComponent<SupportConquerorWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<SupportConquerorWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }
}
