using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSkill : CharacterSkill
{
    private TurretBase TurretBase;
    public TurretBase ShipBase {
        get {
            if(TurretBase == null) {
                TurretBase = CharacterBase as TurretBase;
            }
            return TurretBase;
        }
    }
}
