using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStateController : StateController<DroneBase> {
    private Transition<DroneBase>[] transitions = {ToDieDroneTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<DroneBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(IdleDroneState.Instance);
        IdleDroneState.Instance.StartState(this);
    }
}
