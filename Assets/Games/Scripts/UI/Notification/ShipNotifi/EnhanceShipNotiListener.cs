using System.Collections.Generic;
using UnityEngine;

public class EnhanceShipNotiListener : NotiListener<ShipInfor> {
    [SerializeField] private EnhanceShipCondition[] conditions;
    [SerializeField] private EnhanceShipRegister[] registers;
    public override IEnumerable<GameCondition<ShipInfor>> GetConditions() {
        return conditions;
    }

    public override IEnumerable<NotiRegister<ShipInfor>> GetRegisters() {
        return registers;
    }
}