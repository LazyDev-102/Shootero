using Class_FSM;
using UnityEngine;

public class MB10StateController : StateController<MB10Base> {
    private MB10Transition[] transitions = { MB10IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB10Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB10StartState.Instance);
        MB10StartState.Instance.StartState(this);
    }
}
