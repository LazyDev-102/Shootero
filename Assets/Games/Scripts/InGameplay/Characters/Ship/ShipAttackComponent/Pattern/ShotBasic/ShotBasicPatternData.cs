using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotBasicPatternData", menuName = "Resource/ShipPattern/ShotPattern/ShotBasic")]
public class ShotBasicPatternData : ShipPatternData<ShotBasicPatternInfo> {
}

[Serializable]
public class ShotBasicPatternInfo : ShipPatternInfo {
    [SerializeField] private float speedBullet;
    public float SpeedBullet { get => speedBullet; set => speedBullet = value; }
}