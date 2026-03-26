using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone03Stat : DroneStat {

    [SerializeField] private FloatStat laserDuration;

    public FloatStat LaserDuration { get => laserDuration; }
}
