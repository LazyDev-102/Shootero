using System.Collections.Generic;
using UnityEngine;

public class NewGearNotiListener : NotiListener<GearSoftData> {
    [SerializeField] private NewGearCondition[] conditions;
    [SerializeField] private NewGearRegister[] registers;
    public override IEnumerable<GameCondition<GearSoftData>> GetConditions() {
        return conditions;
    }

    public override IEnumerable<NotiRegister<GearSoftData>> GetRegisters() {
        return registers;
    }
}
