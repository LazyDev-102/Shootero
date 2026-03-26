using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSkill : CharacterSkill
{
    private DroneBase droneBase;
    public DroneBase ShipBase {
        get {
            if(droneBase == null) {
                droneBase = CharacterBase as DroneBase;
            }
            return droneBase;
        }
    }
}
