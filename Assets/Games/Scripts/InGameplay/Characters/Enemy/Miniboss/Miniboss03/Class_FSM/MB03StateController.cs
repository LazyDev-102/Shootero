using Class_FSM;
using UnityEngine;

public class MB03StateController : StateController<MB03Base> {
    private MB03Transition[] transitions = { MB03IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB03Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB03StartState.Instance);
        MB03StartState.Instance.StartState(this);
    }
}
