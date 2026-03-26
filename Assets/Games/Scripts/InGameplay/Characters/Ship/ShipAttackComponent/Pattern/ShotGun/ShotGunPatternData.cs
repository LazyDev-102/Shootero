using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunPatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotGunPatternData")]
public class ShotGunPatternData : ShipPatternData<ShotGunPatternInfo> {

}


[Serializable]
public class ShotGunPatternInfo : ShipPatternInfo {
    [SerializeField] private int numberBullet;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float speedBullet;
    [SerializeField] private RangeFloatValue distance;
    [SerializeField] private RangeFloatValue speedRange;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float SpeedBullet { get => speedBullet; set => speedBullet = value; }
    public int NumberBullet { get => numberBullet; set => numberBullet = value; }
    public float SpreadAngle { get => spreadAngle; set => spreadAngle = value; }
    public RangeFloatValue Distance { get => distance; set => distance = value; }
    public RangeFloatValue SpeedRange { get => speedRange; set => speedRange = value; }
    public float AccelerationSpeed { get => accelerationSpeed; set => accelerationSpeed = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
}