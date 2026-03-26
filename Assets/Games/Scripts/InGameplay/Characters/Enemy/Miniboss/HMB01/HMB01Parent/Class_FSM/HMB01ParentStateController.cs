using Class_FSM;
using UnityEngine;

public class HMB01ParentStateController : StateController<HMB01ParentBase> {
    private HMB01ParentTransition[] transitions = { HMB01ParentIsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<HMB01ParentBase>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(HMB01ParentStartState.Instance);
        HMB01ParentStartState.Instance.StartState(this);
    }
}
