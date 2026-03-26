using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotTwistedPlasmaPatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotTwistedPlasma")]
public class ShotTwistedPlasmaPatternData : ShipPatternData<ShotTwistedPlasmaPatternInfo> {
}


[Serializable]
public class ShotTwistedPlasmaPatternInfo : ShipPatternInfo {
    [SerializeField] private float speedBullet;
    [SerializeField] private int numberBullet;
    [SerializeField] private float halfDistanceBase = 0.5f;
    [SerializeField] private float distanceUpgradeX = 0.2f;
    [SerializeField] private float distanceUpgradeY = 0.03f;
    [SerializeField] private float cycleBase;
    [SerializeField] private float amplitudeBase;
    [SerializeField] private RangeFloatValue cycleRange;
    [SerializeField] private RangeFloatValue amplitudeRange;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float SpeedBullet { get => speedBullet; }
    public float HalfDistanceBase { get => halfDistanceBase; }
    public float DistanceUpgradeX { get => distanceUpgradeX; }
    public float DistanceUpgradeY { get => distanceUpgradeY; }
    public int NumberBullet { get => numberBullet; }
    public float AccelerationSpeed { get => accelerationSpeed; }
    public float MinSpeed { get => minSpeed; }

    public float GetAmplitude(int index) {
        return index == 0 ? amplitudeBase : amplitudeRange.GetRandomValue();
    }
    public float GetCycle(int index) {
        return index == 0 ? cycleBase : cycleRange.GetRandomValue();
    }
}