using Class_FSM;
using UnityEngine;

public class MB14StateController : StateController<MB14Base> {
    private MB14Transition[] transitions = { MB14IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB14Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB14StartState.Instance);
        MB14StartState.Instance.StartState(this);
    }
}
