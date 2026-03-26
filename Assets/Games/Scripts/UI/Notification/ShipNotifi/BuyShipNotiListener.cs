using System.Collections.Generic;
using UnityEngine;

public class BuyShipNotiListener : NotiListener<ShipInfor> {
    [SerializeField] private BuyShipCondition[] conditions;
    [SerializeField] private BuyShipRegister[] registers;
    public override IEnumerable<GameCondition<ShipInfor>> GetConditions() {
        return conditions;
    }

    public override IEnumerable<NotiRegister<ShipInfor>> GetRegisters() {
        return registers;
    }
}
