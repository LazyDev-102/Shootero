using Class_FSM;
using UnityEngine;

public class HMB02StateController : StateController<HMB02Base> {
    private HMB02Transition[] transitions = { HMB02IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<HMB02Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(HMB02StartState.Instance);
        HMB02StartState.Instance.StartState(this);
    }
}
