
using Helper;
using UnityEngine;

public class MinibossConquerorWaveInfo : ConquerorWaveInfo {
    private MinibossConquerorWaveData minibosswaveData;
    private int chipInIcon;

    public MinibossConquerorWaveData MinibossWaveData { get => minibosswaveData; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.minibosswaveData = waveData as MinibossConquerorWaveData;
        chip = RandomHelper.RandomInRange(waveData.RangeChip);
        chipInIcon = this.minibosswaveData.ChipInIcon;
        iconChip = Mathf.CeilToInt(1.0f * chip / chipInIcon);
    }


    public int GetMinibossId() {
        return RandomHelper.RandomInCollection(minibosswaveData.MinibossIds);
    }

    public override int GetChipInIcon() {
        return chipInIcon;
    }

    public override float GetWaveMultipler() {
        return minibosswaveData.WaveMultipler;
    }

    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<MinibossConquerorWaveSpawner>("Wave Spawner");
        }
        MinibossConquerorWaveSpawner newSpawner = spawner.GetComponent<MinibossConquerorWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<MinibossConquerorWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }
}

