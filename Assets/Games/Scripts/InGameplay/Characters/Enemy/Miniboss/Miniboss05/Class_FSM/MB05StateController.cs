using Class_FSM;
using UnityEngine;

public class MB05StateController : StateController<MB05Base> {
    private MB05Transition[] transitions = { MB05IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB05Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB05StartState.Instance);
        MB05StartState.Instance.StartState(this);
    }
}
