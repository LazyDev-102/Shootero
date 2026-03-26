using System;
using UnityEngine;

[Serializable]
public abstract class ConquerorWaveData : ScriptableObject {
    [SerializeField] private WaveCondition[] preStartCondition;
    [SerializeField] private WaveCondition[] preEndCondition;
    [SerializeField] private float waveMultipler;
    [SerializeField] private DifficultWave difficult;
    [SerializeField] private RangeIntValue rangeChip;
    [SerializeField] private RangeIntValue rangeHealOrb;
    [SerializeField] private RangeIntValue rangeMaterial;
    [SerializeField] private RangeIntValue rangeGear;
    [SerializeField] private RangeIntValue rangeReroll;
    [SerializeField] private WaveType waveType;

    public WaveCondition[] PreStartCondition { get => preStartCondition; }
    public WaveCondition[] PreEndCondition { get => preEndCondition; }
    public float WaveMultipler { get => waveMultipler; set => waveMultipler = value; }
    public DifficultWave Difficult { get => difficult; set => difficult = value; }
    public RangeIntValue RangeChip { get => rangeChip; }
    public RangeIntValue RangeHealOrb { get => rangeHealOrb; }
    public RangeIntValue RangeMaterial { get => rangeMaterial; }
    public RangeIntValue RangeGear { get => rangeGear; }
    public RangeIntValue RangeReroll { get => rangeReroll; }
    public WaveType WaveType { get => waveType; }

    public abstract ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex);
}


public enum DifficultWave {
    Easy = 0, Hard = 1, Hell = 2
}