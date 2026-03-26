using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateController : StateController<TurretBase> {
    private Transition<TurretBase>[] transitions = {ToDieTurretTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<TurretBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(IdleTurretState.Instance);
        IdleTurretState.Instance.StartState(this);
    }
}
