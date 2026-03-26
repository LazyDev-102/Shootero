using Class_FSM;
using UnityEngine;

public class HMB01StateController : StateController<HMB01Base> {
    private HMB01Transition[] transitions = { HMB01IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<HMB01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(HMB01StartState.Instance);
        HMB01StartState.Instance.StartState(this);
    }
}
