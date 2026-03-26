using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotGatlingPatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotGatling")]
public class ShotGatlingPatternData : ShipPatternData<ShotGatlingPatternInfo> {

}


[Serializable]
public class ShotGatlingPatternInfo : ShipPatternInfo {
    [SerializeField] private float speedBullet;
    [SerializeField] private float spreadAngle;
    [SerializeField] private int numberBullet;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float SpeedBullet { get => speedBullet; set => speedBullet = value; }
    public float SpreadAngle { get => spreadAngle; set => spreadAngle = value; }
    public int NumberBullet { get => numberBullet; set => numberBullet = value; }
    public float AccelerationSpeed { get => accelerationSpeed; set => accelerationSpeed = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
}
