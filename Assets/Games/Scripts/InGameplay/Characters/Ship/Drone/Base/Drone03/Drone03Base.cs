using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone03Base : DroneBase {
    private Drone03Stat drone03Stat;
    public Drone03Stat Drone03Stat {
        get {
            if (drone03Stat == null) {
                drone03Stat = DroneStat as Drone03Stat;
            }
            return drone03Stat;
        }
    }
}
