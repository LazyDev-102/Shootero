using Helper;
using System.Linq;
using UnityEngine;

public class TutorialWaveInfo : ConquerorWaveInfo { // softData
    private TutorialWaveData tutorialWaveData;
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

    #endregion


    public TutorialWaveData TutorialWaveData { get => tutorialWaveData; }
    public int Limit { get => limit; }
    public int Time { get => time; }
    public float SizePercentEvent { get => sizePercentEvent; set => sizePercentEvent = value; }
    public float LimitPercentEvent { get => limitPercentEvent; set => limitPercentEvent = value; }
    public float HpPercentEvent { get => hpPercentEvent; set => hpPercentEvent = value; }
    public float AtkPercentEvent { get => atkPercentEvent; set => atkPercentEvent = value; }

    public override void CreateData(int currentZoneIndex, int currentWaveIndex, ConquerorWaveData waveData) {
        base.CreateData(currentZoneIndex, currentWaveIndex, waveData);
        this.tutorialWaveData = waveData as TutorialWaveData;
        this.currentWaveIndex = currentWaveIndex;
        this.currentZoneIndex = currentZoneIndex;

        limit = this.tutorialWaveData.NumberE01;
        chip = 6;
        chipInIcon = 2;
        iconChip = chip / chipInIcon;

        GetEventValue();
    }

    public void ChangeChipInfor() {
        chipInIcon = 5;
        iconChip += 1;
    }

    private void GetEventValue() {
        sizePercentEvent = 0;
        limitPercentEvent = 0;
        hpPercentEvent = 0;
        atkPercentEvent = 0;
    }

    public override int GetChipInIcon() {
        return chipInIcon;
    }

    public override ConquerorWaveSpawner SetupSpawner(ConquerorWaveSpawner spawner) {
        if (spawner == null) {
            spawner = GameManager.Instance.GameLoader.Instantiate<TutorialWaveSpawner>("Wave Spawner");
        }
        TutorialWaveSpawner newSpawner = spawner.GetComponent<TutorialWaveSpawner>();
        if (newSpawner == null) {
            newSpawner = spawner.gameObject.AddComponent<TutorialWaveSpawner>();
        }
        newSpawner.SetWaveInfo(this);
        return newSpawner;
    }

    public override float GetWaveMultipler() {
        return tutorialWaveData.WaveMultipler;
    }


}
