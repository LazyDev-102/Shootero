

using UnityEngine;

public abstract class ConquerorWaveInfo {
    protected int chip;
    protected int iconChip;
    protected int spawnedIcon;

    protected int numberWrench;
    protected int spawnedWrench;

    protected int numberMaterial;
    protected int spawnedMaterial;

    protected int numberGear;
    protected int spawnedGear;

    protected int numberReroll;
    protected int spawnedReroll;

    protected WaveType cWaveType;
    private ConquerorWaveData waveData;

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
    public int NumberReroll { get => numberReroll; }
    public int SpawnedReroll { get => spawnedReroll; set => spawnedReroll = value; }
    public int RemainingReroll { get => (numberReroll - spawnedReroll); }
    public WaveType CWaveType { get => cWaveType; }
    public ConquerorWaveData WaveData { get => waveData; }

    public abstract ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner);

    public virtual void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        numberWrench = waveData.RangeHealOrb.GetRandomValue();
        numberGear = waveData.RangeGear.GetRandomValue();
        numberMaterial = waveData.RangeMaterial.GetRandomValue();
        numberReroll = waveData.RangeReroll.GetRandomValue();
        cWaveType = waveData.WaveType;
        this.waveData = waveData;
    }

    public abstract float GetWaveMultipler();

    public abstract int GetChipInIcon();
    public virtual int GetChip(EnemyType eType, int numberChip) {
        return 0;
    }

    public int GetIconCanDrop(int numberDrop) {
        return Mathf.Min(numberDrop, RemainingIcon);
    }

    public int GetWrenchCanDrop(int numberDrop) {
        return Mathf.Min(numberDrop, RemainingWrench);
    }

    public int GetMaterialCanDrop(int numberDrop) {
        return Mathf.Min(numberDrop, RemainingMaterial);
    }

    public int GetGearCanDrop(int numberDrop) {
        return Mathf.Min(numberDrop, RemainingGear);
    }
    public int GetRerollCanDrop(int numberDrop) {
        return Mathf.Min(numberDrop, RemainingReroll);
    }
}
