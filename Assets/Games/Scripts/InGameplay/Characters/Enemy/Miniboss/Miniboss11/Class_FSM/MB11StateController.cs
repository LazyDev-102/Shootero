using Class_FSM;
using UnityEngine;

public class MB11StateController : StateController<MB11Base> {
    private MB11Transition[] transitions = { MB11IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB11Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB11StartState.Instance);
        MB11StartState.Instance.StartState(this);
    }
}
