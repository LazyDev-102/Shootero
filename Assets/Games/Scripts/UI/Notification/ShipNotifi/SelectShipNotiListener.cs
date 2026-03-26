using System.Collections.Generic;
using UnityEngine;

public class SelectShipNotiListener : NotiListener<ShipInfor> {
    [SerializeField] private OpenSelectShipCondition[] openShipConditon;
    [SerializeField] private SelectShipRegister[] selectShipRegisters;

    public override IEnumerable<GameCondition<ShipInfor>> GetConditions() {
        return openShipConditon;
    }

    public override IEnumerable<NotiRegister<ShipInfor>> GetRegisters() {
        return selectShipRegisters;
    }
}
