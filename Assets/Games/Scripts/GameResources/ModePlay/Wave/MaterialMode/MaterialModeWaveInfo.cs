
using Helper;
using UnityEngine;

public class MaterialModeWaveInfo {
    private MaterialModeWaveData waveData;
    private int limit;
    private int time;
    private int chip;
    private int chipInIcon;

    protected int iconChip;
    protected int spawnedIcon;

    protected int numberWrench;
    protected int spawnedWrench;

    protected int numberMaterial;
    protected int spawnedMaterial;

    protected int numberGear;
    protected int spawnedGear;


    public int Chip { get => chip; }
    public int IconChip { get => iconChip; }
    public int SpawnedIcon { get => spawnedIcon; set => spawnedIcon = value; }
    public int RemainingIcon { get => (iconChip - spawnedIcon); }
    public int NumberWrench { get => numberWrench; }
    public int SpawnedWrench { get => spawnedWrench; set => spawnedWrench = value; }
    public int RemainingWrench { get => (numberWrench - spawnedWrench); }
    public int NumberMaterial { get => numberMaterial; }
    public int SpawnedMaterial { get => spawnedMaterial; set => spawnedMaterial = value; }
    public int RemainingMaterial { get => (numberMaterial - spawnedMaterial); }
    public int NumberGear { get => numberGear; }
    public int SpawnedGear { get => spawnedGear; set => spawnedGear = value; }
    public int RemainingGear { get => (numberGear - spawnedGear); }

    public MaterialModeWaveData WaveData { get => waveData; }
    public int Limit { get => limit; }
    public int Time { get => time; }

    public void CreateData(MaterialModeWaveData waveData) {
        this.waveData = waveData;
        numberWrench = waveData.RangeHealOrb.GetRandomValue();
        numberGear = waveData.RangeGear.GetRandomValue();
        numberMaterial = waveData.RangeMaterial.GetRandomValue();

        ConfigModeWave configModeWaves = this.waveData.ConfigModeWaves;

        limit = RandomHelper.RandomInRange(configModeWaves.LimitRange);
        time = RandomHelper.RandomInRange(configModeWaves.TimeRange);
        chip = RandomHelper.RandomInRange(waveData.RangeChip);
        chipInIcon = Mathf.CeilToInt(chip / time);
        if (chipInIcon == 0)
            chipInIcon = 1;
    }

    public int GetChipInIcon() {
        return chipInIcon;
    }

    public MaterialModeWaveSpawner SetupSpawner(MaterialModeWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<MaterialModeWaveSpawner>("Wave Spawner");
        }
        MaterialModeWaveSpawner newSpawner = spawner.GetComponent<MaterialModeWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<MaterialModeWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }

    public int GetBossId() {
        return RandomHelper.RandomInCollection(waveData.BossIds);
    }
    public int GetMiniBossId() {
        return RandomHelper.RandomInCollection(waveData.MinibossIds);
    }
    public float GetWaveMultipler() {
        return waveData.WaveMultipler;
    }
}
