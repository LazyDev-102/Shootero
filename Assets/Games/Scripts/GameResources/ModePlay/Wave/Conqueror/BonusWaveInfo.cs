
using Helper;
using UnityEngine;

public class BonusWaveInfo : ConquerorWaveInfo { // softData
    private BonusWaveData bonusWaveData;
    private int limit;
    private int time;
    private int currentZoneIndex;
    private int currentWaveIndex;
    private int chipInIcon;
    private int maxChestInCamera;
    private int cChest;


    public BonusWaveData BonusWaveData { get => bonusWaveData; }
    public int ChestLimit { get => limit; }
    public int WaveTime { get => time; }
    public int MaxChestInCamera { get => maxChestInCamera; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.bonusWaveData = waveData as BonusWaveData;
        this.currentWaveIndex = currentWaveIndex;
        this.currentZoneIndex = currentZoneIndex;

        limit = RandomHelper.RandomInRange(this.bonusWaveData.ChestCount);
        time = RandomHelper.RandomInRange(this.bonusWaveData.WaveTime);
        chip = RandomHelper.RandomInRange(this.bonusWaveData.RangeChip);
        maxChestInCamera = RandomHelper.RandomInRange(this.bonusWaveData.MaxChestInCamera);
        cChest = 0;
    }

    public override int GetChipInIcon() {
        return chipInIcon;// waveData.GetChip();
    }
    public override int GetChip(EnemyType eType, int numberChip) {
        return bonusWaveData.GetChip(eType, numberChip);
    }
    public void AddChest() {
        cChest++;
    }
    public bool Spawnable() {
        if (cChest > limit)
            return false;
        if (GameManager.Instance.GameLoader.ChestCount() >= maxChestInCamera)
            return false;
        return true;
    }
    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<BonusWaveSpawner>("Wave Spawner");
        }
        BonusWaveSpawner newSpawner = spawner.GetComponent<BonusWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<BonusWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }

    public override float GetWaveMultipler() {
        return bonusWaveData.WaveMultipler;
    }


}
