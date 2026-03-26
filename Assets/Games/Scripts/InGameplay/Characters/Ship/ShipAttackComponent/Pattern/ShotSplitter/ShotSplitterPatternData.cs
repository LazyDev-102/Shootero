using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotSplitterPatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotSplitterPatternData")]
public class ShotSplitterPatternData : ShipPatternData<ShotSplitterPatternInfo> {

}


[Serializable]
public class ShotSplitterPatternInfo : ShipPatternInfo {
    [SerializeField] private int numberBullet;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float speedBullet;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float SpeedBullet { get => speedBullet; set => speedBullet = value; }
    public int NumberBullet { get => numberBullet; set => numberBullet = value; }
    public float SpreadAngle { get => spreadAngle; set => spreadAngle = value; }
    public float AccelerationSpeed { get => accelerationSpeed; set => accelerationSpeed = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
}