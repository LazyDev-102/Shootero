

using Helper;
using UnityEngine;

public class BasicConquerorWaveInfo : ConquerorWaveInfo { // softData
    private BasicConquerorWaveData basicWaveData;
    private int limit;
    private int time;
    private int currentZoneIndex;
    private int currentWaveIndex;
    private int chipInIcon;
    #region EventWaveData
    private float sizePercentEvent;
    private float limitPercentEvent;
    private float hpPercentEvent;
    private float atkPercentEvent;

    private SetEventWaveData curSet;
    #endregion


    public BasicConquerorWaveData BasicWaveData { get => basicWaveData; }
    public int Limit { get => limit; }
    public int Time { get => time; }
    public float SizePercentEvent { get => sizePercentEvent; set => sizePercentEvent = value; }
    public float LimitPercentEvent { get => limitPercentEvent; set => limitPercentEvent = value; }
    public float HpPercentEvent { get => hpPercentEvent; set => hpPercentEvent = value; }
    public float AtkPercentEvent { get => atkPercentEvent; set => atkPercentEvent = value; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.basicWaveData = waveData as BasicConquerorWaveData;
        this.currentWaveIndex = currentWaveIndex;
        this.currentZoneIndex = currentZoneIndex;
        ConfigModeWave configModeWaves = this.basicWaveData.ConfigModeWaves;

        limit = RandomHelper.RandomInRange(configModeWaves.LimitRange);
        time = RandomHelper.RandomInRange(configModeWaves.TimeRange);
        chip = RandomHelper.RandomInRange(waveData.RangeChip);
        chipInIcon = Mathf.CeilToInt((chip * (currentZoneIndex + 1.0f)) / time);
        if (chipInIcon == 0)
            chipInIcon = 1;
        iconChip = Mathf.CeilToInt(1.0f * chip / chipInIcon);

        GetEventValue();
        limit = Mathf.CeilToInt((limit * (1 + limitPercentEvent)));
    }

    private void GetEventValue() {
        sizePercentEvent = 0;
        limitPercentEvent = 0;
        hpPercentEvent = 0;
        atkPercentEvent = 0;
        if (BasicWaveData.SetEventRules != null && BasicWaveData.SetEventRules.Length != 0) {
            curSet = RandomHelper.RandomInCollection(BasicWaveData.SetEventRules);
            curSet.ApplyRules(this);
        }
    }

    public override int GetChipInIcon() {
        return chipInIcon;
    }

    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<BasicConquerorWaveSpawner>("Wave Spawner");
        }
        BasicConquerorWaveSpawner newSpawner = spawner.GetComponent<BasicConquerorWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<BasicConquerorWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }

    public override float GetWaveMultipler() {
        return basicWaveData.WaveMultipler;
    }


}
