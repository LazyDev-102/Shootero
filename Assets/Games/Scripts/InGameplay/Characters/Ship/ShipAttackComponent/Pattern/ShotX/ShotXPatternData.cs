using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotXPatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotXPatternData")]
public class ShotXPatternData : ShipPatternData<ShotXPatternInfo> {

}


[Serializable]
public class ShotXPatternInfo : ShipPatternInfo {
    [SerializeField] private int numberBullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private float firePointDistance;
    [SerializeField] private float angleStart;
    [SerializeField] private float angleDistance;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float minSpeed;

    public float SpeedBullet { get => speedBullet; set => speedBullet = value; }
    public int NumberBullet { get => numberBullet; set => numberBullet = value; }
    public float FirePointDistance { get => firePointDistance; set => firePointDistance = value; }
    public float AngleStart { get => angleStart; set => angleStart = value; }
    public float AngleDistance { get => angleDistance; set => angleDistance = value; }
    public float AccelerationSpeed { get => accelerationSpeed; }
    public float MinSpeed { get => minSpeed; }
}