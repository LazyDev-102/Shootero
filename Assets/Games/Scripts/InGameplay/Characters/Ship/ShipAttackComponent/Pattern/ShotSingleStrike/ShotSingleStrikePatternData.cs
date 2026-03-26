using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotSingleStrikePatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotSingleStrikePatternData")]
public class ShotSingleStrikePatternData : ShipPatternData<ShotSingleStrikePatternInfo> {

}


[Serializable]
public class ShotSingleStrikePatternInfo : ShipPatternInfo {
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSize;
    [SerializeField] private float critRate;
    [SerializeField] private float critDamage;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float BulletSpeed { get => bulletSpeed; }
    public float BulletSize { get => bulletSize; }
    public float CritRate { get => critRate; }
    public float CritDamage { get => critDamage; }
    public float AccelerationSpeed { get => accelerationSpeed; }
    public float MinSpeed { get => minSpeed; }
}