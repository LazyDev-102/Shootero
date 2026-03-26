using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotDoublePatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotDouble")]
public class ShotDoublePatternData : ShipPatternData<ShotDoublePatternInfo> {
}


[Serializable]
public class ShotDoublePatternInfo : ShipPatternInfo {
    [SerializeField] private float speedBullet;
    [SerializeField] private int numberBullet;
    [SerializeField] private float halfDistanceBase = 0.5f;
    [SerializeField] private float distanceUpgradeX = 0.2f;
    [SerializeField] private float distanceUpgradeY = 0.03f;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float SpeedBullet { get => speedBullet; set => speedBullet = value; }
    public float HalfDistanceBase { get => halfDistanceBase; set => halfDistanceBase = value; }
    public float DistanceUpgradeX { get => distanceUpgradeX; set => distanceUpgradeX = value; }
    public float DistanceUpgradeY { get => distanceUpgradeY; set => distanceUpgradeY = value; }
    public int NumberBullet { get => numberBullet; set => numberBullet = value; }
    public float AccelerationSpeed { get => accelerationSpeed; set => accelerationSpeed = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
}